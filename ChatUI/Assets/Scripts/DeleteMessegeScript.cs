using UnityEngine;
using System.Collections;
using UnityScripts;
using System.Net;
using Assets.Scripts.Models;
using UnityScipts;

namespace UnityScripts
{
    public class DeleteMessegeScript : MonoBehaviour
    {

        private float id;
        private MessageManager messageObj;
        private ApplicationManager appManager;

        void Awake()
        {
            this.messageObj = GetComponentInParent<MessageManager>();
            this.appManager = GameObject.FindGameObjectWithTag("Player").GetComponent<ApplicationManager>();
            this.id = messageObj.id;
        }

        public void DeleteMessegeOnClick()
        {
            Debug.Log(ServerInfo.GetDeleteMessegesRoute(this.id.ToString()));
            var request = WebRequest.Create(ServerInfo.GetDeleteMessegesRoute(this.id.ToString())) as HttpWebRequest;
            request.ContentType = ServerInfo.JsonContetnType;
            request.Headers.Add("Authorization", "Bearer " + this.appManager.Token);
            request.Method = "DELETE";
            request.GetResponse();

            var response = (HttpWebResponse)request.GetResponse();
            if(response.StatusCode == HttpStatusCode.OK)
            {
                Destroy(this.transform.parent);
            }
         
        }
    }
}
