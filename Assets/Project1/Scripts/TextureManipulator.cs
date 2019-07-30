using UnityEngine;
using System.Collections;
using System.IO;

public class TextureManipulator : MonoBehaviour
{
    public RenderTexture ResultTexture;
    public int Size = 256;

    /// <summary>
    /// Renderers to Manipulate the materials and show result
    /// </summary>
    public MeshRenderer plane1,plane2,meshToApplyFrom,meshToApplyOn;

    public Material ReplaceMaterial;

    /// <summary>
    /// Reader that is responsible for reading data from xml on disk
    /// </summary>
    private XMLReader textureLoader;

    // Use this for initialization
    void Awake()
    {
        if (ResultTexture == null)
        {
            ResultTexture = new RenderTexture(Size, Size, 0);
            ResultTexture.name = "ResultantTexture";
        }
    }
    
    private void Start()
    {
        this.textureLoader = this.GetComponent<XMLReader>();

        ///Adding Response to delegate that is called after textures loading
        this.textureLoader.OnTexturesLoaded += this.OnTexturesLoaded;
        ///Sending a Call to load the texture data from the disk
        this.textureLoader.LoadData();

        this.meshToApplyFrom.gameObject.hideFlags = HideFlags.HideInHierarchy;
    }

    public void OnTexturesLoaded()
    {

        ///Setting Created Render Textures to the planes 
        this.plane1.material.mainTexture = this.textureLoader.renderTexture1;
        this.plane2.material.mainTexture = this.textureLoader.renderTexture2;

        ///Assigning the Render Textures to the texture channels of our subtraction shader material
        this.meshToApplyFrom.material.SetTexture("_MainTex", this.textureLoader.renderTexture1);
        this.meshToApplyFrom.material.SetTexture("_SecondTex", this.textureLoader.renderTexture2);
    }

    /// <summary>
    /// Button Event Responsible for doing the shader math
    /// </summary>
    public void ButtonEvent()
    {

       CreateNewTexture();

        if (ReplaceMaterial != null)
        {
            this.meshToApplyOn.material = ReplaceMaterial;
            ReplaceMaterial.mainTexture = this.ResultTexture;
        }
    }


    /// <summary>
    /// Method To Create the New Texture from given ones using the graphics library
    /// </summary>
    void CreateNewTexture()
    {
        Renderer renderer = this.meshToApplyFrom.GetComponent<Renderer>();
        Material material = Instantiate(renderer.material);

        Graphics.Blit(material.mainTexture, ResultTexture, material);

        this.WriteTextureToDiskPath(ResultTexture, Application.dataPath + "\\Project1\\Textures\\ResultantTexture\\Texture.png");
    }

    /// <summary>
    /// Writing the Data to the Disk on given path
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="Path"></param>
    public void WriteTextureToDiskPath(RenderTexture texture, string Path)
    {
        ///Mapping the resultant render texture on a simple 2d texture
        Texture2D resultantTexture = new Texture2D(texture.width, texture.height);
        resultantTexture.ReadPixels(new Rect(0, 0, resultantTexture.width, resultantTexture.height), 0, 0, false);
        resultantTexture.Apply();

        ///Encoding texture to bytes
        byte[] data = resultantTexture.EncodeToPNG();

        ///Writing bytes data to disk
        FileStream fstream = File.Open(Path, FileMode.OpenOrCreate);
        BinaryWriter st = new BinaryWriter(fstream);
        st.Write(data);
        fstream.Close();
    }
}
