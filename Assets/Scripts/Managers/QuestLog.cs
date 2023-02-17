using System;

namespace InventoryQuest
{
    public static class QuestLog
    {
        public static event EventHandler<string> OnNewsReceived;

        public static void Log(string newsItem)
        {
            OnNewsReceived?.Invoke(null, newsItem);
        }
    }
}
