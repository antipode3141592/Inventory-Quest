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
            if (newsItems.Count > 0)
                StartCoroutine(ScrollNews(newsItems.Dequeue()));
        }

        void OnNewsReceivedHandler(object sender, string e)
        {
            newsItems.Enqueue(e);
            
        }

        IEnumerator ScrollNews(string news)
        {
            Debug.Log(news);
            newsItemDisplays[0].SetText(news);
            newsItemDisplays[0].Show();
            yield return new WaitForSeconds((float)news.Length/ scrollCharactersPerSecond);
            newsItemDisplays[0].Hide();
            yield return new WaitForSeconds(0.3f);
        }
    }
}
