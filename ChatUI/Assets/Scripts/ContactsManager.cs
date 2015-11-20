using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Net;
using System.IO;
using LitJson;
using System.Linq;
using Assets.Scripts.Models;

namespace UnityScripts
{
    public class ContactsManager : MonoBehaviour
    {
        public GameObject UserModel;
        public ApplicationManager appManager;

        private string accessToken;
        private int count;
        private float currentTime;
        private float intervalTime;
        // Use this for initialization
        void Awake()
        {
            this.appManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ApplicationManager>();
            this.accessToken = this.appManager.Token;

            this.GenerateUsers(this.GetUsersFromSercer());
            this.currentTime = 0;
            this.intervalTime = 0.5f;
        }

        // Update is called once per frame
        void Update()
        {
            this.currentTime += Time.deltaTime;

            if (currentTime >= this.intervalTime)
            {
                var userListFromServer = this.GetUsersFromSercer();
                if(userListFromServer.Count!=this.count)
                {
                    this.GenerateUsers(userListFromServer);
                }
                this.currentTime = 0;
            }
        }

        private void GenerateUsers(List<string> userNames)
        {
            var children = new List<GameObject>();
            foreach (Transform child in transform) children.Add(child.gameObject);
            children.ForEach(child => Destroy(child));

            foreach (var item in userNames)
            {
                var user = Instantiate(UserModel);
                user.transform.SetParent(this.gameObject.transform, false);
                var text = user.GetComponentInChildren<Text>();
                text.text = item;
            }
        }

        private List<string> GetUsersFromSercer()
        {
            var request = WebRequest.Create(ServerInfo.GetUsersRoute()) as HttpWebRequest;
            request.ContentType = ServerInfo.JsonContetnType;
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            request.Method = "GET";
            request.GetResponse();

            var response = (HttpWebResponse)request.GetResponse();

            var reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();
            reader.Close();

            var resultJ = JsonMapper.ToObject<List<UserModel>>(result);

            var userList = resultJ.Select(x => x.UserName).ToList();
            this.count = userList.Count;
            return userList;
        }
    }
}