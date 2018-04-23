using System;
using System.Collections.Generic;
using System.Diagnostics;
using Quobject.SocketIoClientDotNet.Client;
using Newtonsoft.Json;

namespace Assets.Scripts.Server
{

    public delegate void Print(object msg);
    public delegate void Load();

    public class JSWrapper
    {
        private const string serverURL = "http://localhost:3000";
        private const string executableName = "serverCom.exe";
        private Process process;
        private Print print;
        private Load load;
        private List<int> PIDs;

        public JSWrapper(Print _print, Load _load)
        {
            print = _print;
            load = _load;
            PIDs = new List<int>();
            print("launching electron..");
            process = ExecuteCommand("%appdata%\\..\\Local\\ServerComs\\"+executableName+" & tasklist /FI \"IMAGENAME eq "+executableName+"\" /FO CSV /NH");
            print("electron launched");
        }
        public void openConnection()
        {
            if (Master.Socket == null)
            {
                print("Attempting to connect to " + serverURL);
                Master.Socket = IO.Socket(serverURL);
                Master.Socket.On(Socket.EVENT_CONNECT, () => load());
            }
        }
        Process ExecuteCommand(string command)
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            Process process = Process.Start(processInfo);

            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
                if (e.Data.Contains("*loaded*")) openConnection();
                else if (e.Data.Contains("\""+executableName+"\"")) setPID(e.Data);
                else print("output>>" + e.Data);
            };
            process.BeginOutputReadLine();

            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                print("error>>" + e.Data);
            process.BeginErrorReadLine();

            process.Exited += (object sender, EventArgs e) =>
                print("Exit>>" + e.ToString());

            return process;
        }

        void setPID(string msg)
        {
            string _PID = msg.Split(',')[1].Replace('"', ' ');
            int PID = 0;
            int.TryParse(_PID, out PID);
            if (PID != 0) PIDs.Add(PID);
            else print("ERROR: Cant find process in msg " + msg);
        }

        public JSWrapper On(string eventString, Action<object> action)
        {
            Master.Socket.Emit("__makeHandler", eventString);
            Master.Socket.On(eventString, action);
            return this;
        }
        public JSWrapper Emit(string title, string msg = null)
        {
            string data = JsonConvert.SerializeObject(new MessageDTO(title, msg));
            Master.Socket.Emit("__sendToServer", data);
            return this;
        }
        public JSWrapper Emit(string title, object msg)
        {
            return Emit(title, JsonConvert.SerializeObject(msg));
        }
        public void ForceClose()
        {
            print("Closing ServerCom..");
            process.Close();
            foreach (var PID in PIDs) try
                {
                    Process.GetProcessById(PID).Kill();
                }
                catch (Exception e) { }
                
            if(Master.Socket != null) Master.Socket.Close();
            print("ServerCom closed");
        }
    }
    class MessageDTO
    {
        public string title;
        public string msg;
        public MessageDTO(string title, string msg)
        {
            this.title = title;
            this.msg = msg;
        }
    }
}