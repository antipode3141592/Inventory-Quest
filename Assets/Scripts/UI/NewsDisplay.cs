using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryQuest.UI
{
    public class NewsDisplay : MonoBehaviour
    {
        [SerializeField] float scrollCharactersPerSecond = 27f;
        [SerializeField] List<NewsItem> newsItemDisplays;

        Queue<string> newsItems = new();

        void Start()
        {
            foreach (var item in newsItemDisplays)
                item.Hide();

            QuestLog.OnNewsReceived += OnNewsReceivedHandler;
        }

        void Update()
        {
            if (newsItems.Count > 0 && ScrollAvailable())
                StartCoroutine(ScrollNews(newsItems.Dequeue()));
        }

        bool ScrollAvailable()
        {
            for (int i = 0; i < newsItemDisplays.Count; i++)
                if (!newsItemDisplays[i].IsActive)
                    return true;
            return false;
        }

        void OnNewsReceivedHandler(object sender, string e)
        {
            newsItems.Enqueue(e);
        }

        IEnumerator ScrollNews(string news)
        {
            Debug.Log(news);
            for (int i = 0; i < newsItemDisplays.Count; i++)
            {
                if (newsItemDisplays[i].IsActive)
                    continue;
                newsItemDisplays[i].SetText(news);
                newsItemDisplays[i].Show();
                yield return new WaitForSeconds((float)news.Length / scrollCharactersPerSecond);
                newsItemDisplays[i].Hide();
                yield return new WaitForSeconds(0.3f);
                newsItemDisplays[i].IsActive = false;
                break;  
            }
        }
    }
}
