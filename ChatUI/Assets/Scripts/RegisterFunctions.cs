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
        public Text FirstName;
        public Text LastName;
        public Text UserName;
        public Text Password;
        public Text ConfirmPass;

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
            var request = WebRequest.Create(ServerInfo.GetRegisterRoute()) as HttpWebRequest;

            request.ContentType = "application/json";
            request.Method = "POST";

            var jsonData = JsonMapper.ToJson(this.MakerRegistrationEntry());
            Debug.Log(jsonData);
            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(jsonData);
            }

            var response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Application.LoadLevel("LogIn");
            }
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





