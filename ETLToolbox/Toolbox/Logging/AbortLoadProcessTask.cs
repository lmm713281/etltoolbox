﻿using System;


namespace ALE.ETLToolbox {
    public class AbortLoadProcessTask : GenericTask, ITask {
        /* ITask Interface */
        public override string TaskType { get; set; } = "LOADPROCESS_ABORT";
        public override string TaskName => $"Abort process with key {LoadProcessKey}";
        public override void Execute() {
            new SqlTask(this, Sql) { DisableLogging = true }.ExecuteNonQuery();
            var rlp = new ReadLoadProcessTableTask(LoadProcessKey) { TaskType = this.TaskType, TaskHash = this.TaskHash, DisableLogging = true };            
            rlp.Execute();
            ControlFlow.CurrentLoadProcess = rlp.LoadProcess;
        }

        /* Public properties */
        public int? _loadProcessKey;
        public int? LoadProcessKey {
            get {
                return _loadProcessKey ?? ControlFlow.CurrentLoadProcess?.LoadProcessKey;
            }
            set {
                _loadProcessKey = value;
            }
        }
        public string AbortMessage { get; set; }


        public string Sql => $@"EXECUTE etl.AbortLoadProcess
	 @LoadProcessKey = '{LoadProcessKey ?? ControlFlow.CurrentLoadProcess.LoadProcessKey}',
	 @AbortMessage = {AbortMessage.NullOrParenthesisString()}";

        public AbortLoadProcessTask() {

        }

        public AbortLoadProcessTask(int? loadProcessKey) : this() {
            this.LoadProcessKey = loadProcessKey;
        }
        public AbortLoadProcessTask(int? loadProcessKey, string abortMessage) : this(loadProcessKey) {
            this.AbortMessage = abortMessage;
        }

        public AbortLoadProcessTask(string abortMessage) : this() {
            this.AbortMessage = abortMessage;
        }

        public static void Abort() => new AbortLoadProcessTask().Execute();
        public static void Abort(int? loadProcessKey) => new AbortLoadProcessTask(loadProcessKey).Execute();
        public static void Abort(string abortMessage) => new AbortLoadProcessTask(abortMessage).Execute();
        public static void Abort(int? loadProcessKey, string abortMessage) => new AbortLoadProcessTask(loadProcessKey, abortMessage).Execute();


    }
}
