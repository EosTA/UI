using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Net;
using System;
using System.IO;
using LitJson;
using Assets.Scripts.Models;

namespace UnityScripts
{
    public class RegisterFunctions : MonoBehaviour
    {
        public InputField FirstName;
        public InputField LastName;
        public InputField UserName;
        public InputField Password;
        public InputField ConfirmPass;
        public Image Error;

        private bool ValidateTextField(string text, int minLenght, int maxLenght)
        {
            if (text.Length < minLenght || text.Length > maxLenght)
            {
                return false;
            }

            return true;
        }

        private bool ValidatePasswords(string pass, int minLenght, int maxLenght)
        {
            if (pass.Length < minLenght || pass.Length > maxLenght)
            {
                return false;
            }

            return true;
        }

        public void OnRegisterClick()
        {
            Debug.Log("Hello");
            try
            {
                Debug.Log(1);
                var request = WebRequest.Create(ServerInfo.GetRegisterRoute()) as HttpWebRequest;
                Debug.Log(2);
                request.ContentType = ServerInfo.JsonContetnType;
                request.Method = "POST";
                Debug.Log(3);
                var jsonData = JsonMapper.ToJson(this.MakerRegistrationEntry());
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(jsonData);
                }
                Debug.Log(4);
                var response = (HttpWebResponse)request.GetResponse();
                Debug.Log(5);
                Application.LoadLevel("LogIn");
            }
            catch
            {
                this.Error.gameObject.SetActive(true);
            }

        }

        public void OnBackClick()
        {
            Debug.Log("Hello");
            Application.LoadLevel("LogIn");
        }

        private RegisterInfoModel MakerRegistrationEntry()
        {
            return new RegisterInfoModel
            {
                Email = this.UserName.text,
                Password = this.Password.text,
                ConfirmPassword = this.ConfirmPass.text,
                FirstName = this.FirstName.text,
                LastName = this.LastName.text
            };
        }

    }
}





