using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;
using EasyTextEffects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Jobs;
using System.Collections;
using EasyTextEffects.Effects;


//TODO make a hybrid class that contains both the effect type and the attribute that it corresponds to 
public class text_spawner : MonoBehaviour
{
    public float Delay = 0.5f ; 
    //public List<GlobalTextEffectEntry> effectsList; //create corresponding affects for each enchantment
    //public enchantmentsList = //will update with proper datatype once it gets typed
    public GameObject text; //this is the text prefab
    private List<GameObject> textList; 
    public TextEffectInstance showcase_effect;
    public float offset_amount = 25;
    public Canvas canvas;
    public Vector3 location;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //create function calls 
        List<string> list = new List<string>()
        {
            "strength 1",
            "shiny 1",
            "wisdom 1",
            "durability 1",
            "indestrutable 1"
        };
        textList = MakeTextBoxList(list); //!!

        foreach (GameObject item in textList) // attempting to refresh all text effects before utilization
        {
            TextEffect currentTextEffect = item.GetComponent<TextEffect>(); // grab current text effect item
            currentTextEffect.Refresh();
        }

        foreach (GameObject textIter in textList) //iterates through and plays each affect after a delay 
        {
            TextEffect currentTextEffect = textIter.GetComponent<TextEffect>();
            
            currentTextEffect.StartManualEffects();
            //yield return new WaitForSeconds(Delay);
        }
        //StartCoroutine(PlayEffects(textList));
    }

    // Update is called once per frame
    void Update()
    {

        //behavior if list of attributes goes past the scene
            //keep space constant but make text smaller or something idk 
        //activate Playeffects when a button is pressed
        
    }

    // make a text box and change its effect to the desired effect
    // return: reference to the instanitated text box
    // input: effectType and attribute(items attributes list)
    GameObject MakeTextBox(GlobalTextEffectEntry effectType, string attribute, Vector3 position)
    {
        //create object and grab the global effects list
        GameObject currentText = Instantiate(text); //only makes one copy
        //update the current text box position in world space
        currentText.transform.position = position;

        TextEffect currentTextEffect = currentText.GetComponent<TextEffect>();
        //refer to the texts globaleffects
        List<GlobalTextEffectEntry> effects = currentTextEffect.globalEffects;
        Debug.Assert(effectType != null);
        //populate current global effects list for the one object
        foreach (GlobalTextEffectEntry effect in effects) //!!
        {
            //force effect to be added as manual if not 
            if (effect.triggerWhen != TextEffectEntry.TriggerWhen.Manual)
            {
                effect.triggerWhen = TextEffectEntry.TriggerWhen.Manual;
            }
        }

        //change basic text to attribute text 
        TextMeshProUGUI currentTextMeshPro = currentText.GetComponent<TextMeshProUGUI>();
        currentTextMeshPro.text = attribute;

        return currentText;
    }
    /// <summary>
    /// this function is makes a list of text boxes from the relvant attributes, currently it only works with a default effect type and doesn't reference the item class attributes
    /// </summary>
    /// <returns>A list of game objects that are tmps </returns>
    List<GameObject> MakeTextBoxList(List<string>refer2_classEffectltr)
    {
        float offset = 0;
        //intialize default text effect may replace later
        GlobalTextEffectEntry default_effect = new GlobalTextEffectEntry();
        default_effect.triggerWhen = TextEffectEntry.TriggerWhen.Manual;
        default_effect.effect = showcase_effect;
        //Gameobject containing the text 
        List<GameObject> textBoxList = new List<GameObject>();

        //interates through the attributes and creates a text box for each one
        foreach (string attribute in refer2_classEffectltr)
        {
            GameObject temp = MakeTextBox(default_effect, attribute, location); //!!
            temp.transform.SetParent(canvas.transform);
            temp.transform.localPosition = new Vector3(0,offset,0); 
            textBoxList.Add(temp);
            offset -= offset_amount;

        }
        return textBoxList;

    }

    
    //IEnumerator PlayEffects(List<GameObject> textBoxList)
    //{
    //    foreach (GameObject textIter in textBoxList) //iterates through and plays each affect after a delay 
    //    {
    //        TextEffect currentTextEffect = textIter.GetComponent<TextEffect>();

    //        currentTextEffect.StartManualEffects();
    //        yield return new WaitForSeconds(Delay);
    //    }

    //}
}
