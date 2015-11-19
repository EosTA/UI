using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Net;
using System.IO;
using LitJson;
using System.Linq;
using UnityScipts;
using Assets.Scripts.Models;

namespace UnityScripts
{
    public class ContactsManager : MonoBehaviour
    {
        public GameObject UserModel;
        public ApplicationManager appManager;

        private string accessToken;
        // Use this for initialization
        void Awake()
        {
            this.appManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ApplicationManager>();
            this.accessToken = this.appManager.Token;

            this.GenerateUsers(this.GetUsersFromSercer());
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void GenerateUsers(List<string> userNames)
        {
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

            var request = WebRequest.Create("http://localhost:50619/api/users") as HttpWebRequest;
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            request.Method = "GET";
            request.GetResponse();

            var response = (HttpWebResponse)request.GetResponse();

            var reader = new StreamReader(response.GetResponseStream());
            var result = reader.ReadToEnd();
            reader.Close();

            var resultJ = JsonMapper.ToObject<List<UserModel>>(result);

            return resultJ.Select(x => x.UserName).ToList();

        }
    }
}