using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.IO;
using LitJson;
using UnityEngine.UI;
using Assets.Scripts.Models;

namespace UnityScripts
{
    public class LoginInScreenFunctions : MonoBehaviour
    {
        public InputField UserName;
        public InputField Password;
        public Image Error;
        public ApplicationManager appManager;


        public void LogIn()
        {

            try
            {
                var request = WebRequest.Create(ServerInfo.GetLoginRoute()) as HttpWebRequest;

                request.ContentType = ServerInfo.StringQueryType;
                request.Method = "POST";

                var jsonData = this.MakeLogInEntry().ToString();
                Debug.Log(jsonData);

                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(jsonData);
                }

                var response = (HttpWebResponse)request.GetResponse();

                var reader = new StreamReader(response.GetResponseStream());
                var result = reader.ReadToEnd();
                reader.Close();
                var token = JsonMapper.ToObject(result)["access_token"].ToString();

                appManager.Token = token;
                Application.LoadLevel("Chat");
            }
            catch
            {
                this.Error.gameObject.SetActive(true);
                this.UserName.text = string.Empty;
                this.Password.text = string.Empty;
            }

        }

        public void EnterRegistration()
        {
            Application.LoadLevel("Register");
        }

        private LogInModel MakeLogInEntry()
        {
            return new LogInModel
            {
                Email = this.UserName.text,
                Password = this.Password.text,
            };
        }
    }
}

