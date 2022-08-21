using System.Threading.Tasks;
using UnityEngine;

namespace Mvc.Core
{
    public class YieldTask : CustomYieldInstruction
    {
        public YieldTask(Task task)
        {
            Task = task;
        }

        public override bool keepWaiting => !Task.IsCompleted;

        public Task Task { get; }
    }
}