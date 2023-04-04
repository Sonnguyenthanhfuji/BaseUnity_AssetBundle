using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Net;
using System.IO;

public class BundleWebLoader : MonoBehaviour
{
    public static BundleWebLoader instance = null;

    string bundleUrl; //unlock
    string bundleUrlKira;
    string bundleUrlLock; //lock
    string bundleUrlColectionKira;
    string assetName = "1"; //1.png
    string pathurlNomal= "imageandroidcardnomal", pathurlLock= "imageandroidlock", pathurlKira= "imageandroidkira", pathurlColectionKira = "colectioncardunkira";   
    public Sprite newSprite;
    public Texture tex1;
    private string AccesToken;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
#if UNITY_ANDROID
        bundleUrl =     "https://asset-files.s3.ap-northeast-1.amazonaws.com/AssetBundle/imagecardnomalandroid"; //nomal
        bundleUrlLock = "https://asset-files.s3.ap-northeast-1.amazonaws.com/AssetBundle/imagelockandroid";// lock
        bundleUrlKira = "https://asset-files.s3.ap-northeast-1.amazonaws.com/AssetBundle/imagekiraandroid";// Kira
        bundleUrlColectionKira = "https://asset-files.s3.ap-northeast-1.amazonaws.com/AssetBundle/colectioncardunkiraandroid";
        Debug.Log(" AB //" + bundleUrl);
#elif UNITY_EDITOR
        bundleUrl =              "https://asset-files.s3.ap-northeast-1.amazonaws.com/AssetBundle/imagecardnomalandroid"; //nomal
        bundleUrlLock =          "https://asset-files.s3.ap-northeast-1.amazonaws.com/AssetBundle/imagelockandroid";// lock
        bundleUrlKira =          "https://asset-files.s3.ap-northeast-1.amazonaws.com/AssetBundle/imagekiraandroid";// Kira
        bundleUrlColectionKira = "https://asset-files.s3.ap-northeast-1.amazonaws.com/AssetBundle/colectioncardunkiraandroid";
        Debug.Log(" AB //"+ bundleUrl);
#else    //IOS 
        bundleUrl =              "https://asset-files.s3.ap-northeast-1.amazonaws.com/AssetBundle/imagecardnomal"; // nomal
        bundleUrlLock =          "https://asset-files.s3.ap-northeast-1.amazonaws.com/AssetBundle/imagelock";// lock
        bundleUrlKira=           "https://asset-files.s3.ap-northeast-1.amazonaws.com/AssetBundle/imagekira"; //kira 
        bundleUrlColectionKira = "https://asset-files.s3.ap-northeast-1.amazonaws.com/AssetBundle/colectioncardunkiraios";
#endif

        if (PlayerPrefs.HasKey("CheckAB"))   //
        {
            if (PlayerPrefs.GetInt("CheckAB") > 0)
            {
                //LoadABLocal();
                Debug.Log("//==-- GetInt >= 4");
            }
            else
            {
                // hoac gan lai PlayerPrefs TokenAccset neu co HasKey  PlayerPrefs.SetString(Constants.KEY_ACCESS_TOKEN, accesToken);
                Debug.Log("//==-- GetInt  4");
                //AccesToken = PlayerPrefs.GetString(Constants.KEY_ACCESS_TOKEN);

                //PlayerPrefs.DeleteAll();
                //LoadASB();
                //GameObject.Find("SplashScreenManager").GetComponent<SplashScreenManager>().StartDownload();

                //PlayerPrefs.SetString(Constants.KEY_ACCESS_TOKEN, AccesToken);

                //PlayerPrefs.SetInt("CheckAB",0);
            }
            //Controller.instance.ABDone = true;
            //StartCoroutine(LoadAB());
            //LoadASB();
        }
        else  // Khi bam button download moi download aseetB
        {
            //LoadABLocal();
            //LoadASB();
            Debug.Log("Done DownLoadABOKOK//////////");
        }
    }

    public void LoadASB()
    {
        StartCoroutine(LoadAB());
    }

    public void LoadABLocal()
    {
        AssetBundleCreateRequest rNomal = AssetBundle.LoadFromFileAsync(Application.persistentDataPath + "/" + pathurlNomal);//Application.streamingAssetsPath + "/" + CurrentPlatformString() + "/" + bundleName);

        AssetBundleCreateRequest rLock = AssetBundle.LoadFromFileAsync(Application.persistentDataPath + "/" + pathurlLock);//Application.streamingAssetsPath + "/" + CurrentPlatformString() + "/" + bundleName);

        AssetBundleCreateRequest rKira = AssetBundle.LoadFromFileAsync(Application.persistentDataPath + "/" + pathurlKira);//Application.streamingAssetsPath + "/" + CurrentPlatformString() + "/" + bundleName);

        AssetBundleCreateRequest rColectionKira = AssetBundle.LoadFromFileAsync(Application.persistentDataPath + "/" + pathurlColectionKira);//Application.streamingAssetsPath + "/" + CurrentPlatformString() + "/" + bundleName);

        if (Controller.instance.bundleLock == null)
        {
            Controller.instance.bundleLock = rLock.assetBundle;
        }
        if (Controller.instance.bundleUnLock == null)
        {
            Controller.instance.bundleUnLock = rNomal.assetBundle;
        }
        if (Controller.instance.bundleKira == null)
        {
            Controller.instance.bundleKira = rKira.assetBundle;
        }
        if (Controller.instance.bundleColectionKira == null)
        {
            Controller.instance.bundleColectionKira = rColectionKira.assetBundle;
        }
    }

    IEnumerator SaveAndDownload(string path, string url)
    {
        string saveTo = Application.persistentDataPath + "/"+ path;

        WWW www = new WWW(url);
        yield return www;
        byte[] bytes = www.bytes;
        File.WriteAllBytes(saveTo, bytes);
        yield return new WaitForSeconds(1);
        Debug.Log("Load "+ Application.persistentDataPath + "/");
        Controller.instance.checkABDone += 1;
        StartCoroutine(SaveAndDownloadKira(pathurlKira, bundleUrlKira));//(pathurlKira, bundleUrlKira)); 3

        PlayerPrefs.SetInt("CheckAB", Controller.instance.checkABDone);
    }

    IEnumerator SaveAndDownload1(string path, string url)
    {
        string saveTo = Application.persistentDataPath + "/" + path;

        WWW www = new WWW(url);
        yield return www;
        byte[] bytes = www.bytes;
        File.WriteAllBytes(saveTo, bytes);
        yield return new WaitForSeconds(1);
        Debug.Log("Load " + Application.persistentDataPath + "/");
        Controller.instance.checkABDone = 4;
        //StartCoroutine(SaveAndDownloadKira(pathurlKira, bundleUrlKira));//(pathurlKira, bundleUrlKira)); 3

        PlayerPrefs.SetInt("CheckAB", Controller.instance.checkABDone);
    }

    IEnumerator SaveAndDownloadKira(string path1, string url1)
    {
        path1 = "imageandroidkira";
        string saveTo1 = Application.persistentDataPath + "/" + path1;
        WWW www1 = new WWW(url1);
        yield return www1;
        byte[] bytes = www1.bytes;
        File.WriteAllBytes(saveTo1, bytes);
        yield return new WaitForSeconds(1);
        Debug.Log("Load");
        Controller.instance.checkABDone = 3;
        StartCoroutine(SaveAndDownload1(pathurlNomal, bundleUrl));//  (pathurlNomal, bundleUrl)); 4

        PlayerPrefs.SetInt("CheckAB", Controller.instance.checkABDone); 
    }
    IEnumerator SaveAndDownloadColectionKira(string pathc, string urlc)
    {
        string saveToc = Application.persistentDataPath + "/" + pathc;
        WWW wwwc = new WWW(urlc);
        yield return wwwc;
        byte[] bytes = wwwc.bytes;
        File.WriteAllBytes(saveToc, bytes);
        yield return new WaitForSeconds(1);
        Debug.Log("Load");
        Controller.instance.checkABDone = 1;
        PlayerPrefs.SetInt("CheckAB", Controller.instance.checkABDone);
        StartCoroutine(SaveAndDownload(pathurlLock, bundleUrlLock));  //(pathurlLock, bundleUrlLock)); 1

        //if (Controller.instance.checkABDone >= 4)
        //{
        //LoadABLocal();
        //}
    }


    IEnumerator LoadAB()
    {
        string pathurl = "imageioscardnomal";
        //StartCoroutine(LoadWebLock(pathurlLock, bundleUrlLock));
        StartCoroutine(LoadWebColectionKira(pathurlColectionKira, bundleUrlColectionKira));

        //StartCoroutine(LoadWebBigICharacter(pathurlBigICharacter, bundleUrlBigICharacter));
        yield return new WaitForSeconds(1); 
    }

    IEnumerator LoadNomal(string path222, string bundle_Url222)
    {
        PlayerPrefs.SetInt("CheckAB", 0);

        yield return new WaitForSeconds(0.1f);
        using (WWW web222 = new WWW(bundle_Url222)) //nomal
        {
            yield return web222;
            //StartCoroutine(SaveAndDownload(path222, bundle_Url222));//  (pathurlNomal, bundleUrl)); 2
            Controller.instance.bundleUnLock = web222.assetBundle;
            StartCoroutine(LoadWebKira(pathurlKira, bundleUrlKira));

        }

    }
    IEnumerator LoadWebLock(string path2,string bundle_Url2)
    {
        yield return new WaitForSeconds(0.1f);
        using (WWW webload2 = new WWW(bundle_Url2))
        {
            yield return webload2;
            //StartCoroutine(SaveAndDownload(path2, bundle_Url2));   //(pathurlLock, bundleUrlLock)); 1
            Controller.instance.bundleLock = webload2.assetBundle;
            StartCoroutine(LoadNomal(pathurlNomal, bundleUrl));
            
        }
    }
    IEnumerator LoadWebKira(string path1,string bundle_Url1)
    {
        yield return new WaitForSeconds(0.1f);
        using (WWW webload1 = new WWW(bundle_Url1))
        {
            yield return webload1;
            //StartCoroutine(SaveAndDownloadKira(path1, bundle_Url1));//(pathurlKira, bundleUrlKira)); 3
            Controller.instance.bundleKira = webload1.assetBundle;
            StartCoroutine(SaveAndDownloadColectionKira(pathurlColectionKira, bundleUrlColectionKira));  //(pathurlColectionKira, bundleUrlColectionKira)); 4
            //StartCoroutine(LoadWebColectionKira(pathurlColectionKira, bundleUrlColectionKira));
        }

    }
    IEnumerator LoadWebColectionKira(string pathc, string bundle_Urlc)
    {
        PlayerPrefs.SetInt("CheckAB", 0);
        yield return new WaitForSeconds(0.1f);
        using (WWW webloadc = new WWW(bundle_Urlc))
        {
            yield return webloadc;
            Controller.instance.bundleColectionKira = webloadc.assetBundle;
            yield return new WaitForSeconds(5.1f);
            StartCoroutine(LoadWebLock(pathurlLock, bundleUrlLock));

            //StartCoroutine(SaveAndDownloadColectionKira(pathc, bundle_Urlc));  //(pathurlColectionKira, bundleUrlColectionKira)); 4

            //Controller.instance.ABDone = true;
        }
    }
}