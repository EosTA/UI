using UnityEngine;
using System.Collections;
using System.Net;
using Assets.Scripts.Models;
using UnityScripts;

namespace UnityScripts
{
    public class DeleteMessegeScript : MonoBehaviour
    {

        public int id;
        private MessageManager messageObj;
        private ApplicationManager appManager;
        public GameObject parrent;

        void Awake()
        {
            this.appManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ApplicationManager>();
            parrent = transform.parent.gameObject;
            this.messageObj = parrent.GetComponent<MessageManager>();            
            this.id = messageObj.Id;
        }

        void Update()
        {
            this.id = messageObj.Id;
        }

        public void DeleteMessegeOnClick()
        {
            //Debug.Log(ServerInfo.GetDeleteMessegesRoute(this.id.ToString()));
            var request = WebRequest.Create(ServerInfo.GetDeleteMessegesRoute(this.id.ToString())) as HttpWebRequest;
            request.ContentType = ServerInfo.JsonContetnType;
            request.Headers.Add("Authorization", "Bearer " + this.appManager.Token);
            request.Method = "DELETE";
            request.GetResponse();

            var response = (HttpWebResponse)request.GetResponse();
            if(response.StatusCode == HttpStatusCode.OK)
            {
                Destroy(this.transform.parent.gameObject);
            }
         
        }
    }
}
