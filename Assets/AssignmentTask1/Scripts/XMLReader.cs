using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.IO;

namespace QuixelTest.SubtractionShaderAssignment
{
    public class XMLReader : MonoBehaviour
    {
        public TextAsset xmlFile;
        public Texture2D texture1, texture2;

        public RenderTexture renderTexture1, renderTexture2;

        public SimpleDelegate OnTexturesLoaded;

        private byte[] textureData1, textureData2;
        [SerializeField] private string Data;

        public Button button;

        private void OnEnable()
        {
            this.OnTexturesLoaded += this.TexturesLoaded;
        }

        public void LoadData()
        {
            this.ParseData(this.xmlFile.text);
        }

        void TexturesLoaded()
        {
            if (this.button)
            {
                this.button.interactable = true;
            }
            Debug.Log("Textures Loaded Successfully");
        }


        /// <summary>
        /// Loading the Data from the given xml 
        /// </summary>
        /// <param name="data"></param>
        public void ParseData(string data)
        {
            string textureLinkFormat = "//Textures/Texture";

            XmlDocument xml = new XmlDocument();
            xml.Load(new StringReader(data));

            XmlNodeList list = xml.SelectNodes(textureLinkFormat);

            this.texture1 = new Texture2D(1024, 1024);
            this.texture2 = new Texture2D(1024, 1024);

            string tex1 = Application.dataPath + list[0].InnerText;
            string tex2 = Application.dataPath + list[1].InnerText;

            if (File.Exists(tex1))
                this.textureData1 = File.ReadAllBytes(tex1);
            if (File.Exists(tex2))
                this.textureData2 = File.ReadAllBytes(tex2);

            this.texture1.LoadImage(this.textureData1);
            this.texture2.LoadImage(this.textureData2);

            Graphics.Blit(this.texture1, this.renderTexture1);
            Graphics.Blit(this.texture2, this.renderTexture2);

            this.OnTexturesLoaded();
        }
    }
}