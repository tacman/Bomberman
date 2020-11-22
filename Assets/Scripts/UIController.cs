using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    private VisualElement root;
    private Slider rage;
    private Label label;
    private VisualElement mainMenu;
    private IMGUIContainer settingsUI;

    public void OnEnable() {
        
        
        // https://forum.unity.com/threads/how-can-we-attach-script-to-our-uxml-uss-at-runtime.872242/
        root = GetComponent<UIDocument>().rootVisualElement;
        
        Debug.Log(root.name + "/" + root.viewDataKey);
        
        
        var buttons = root.Q<Button>();
        buttons.clickable = new Clickable(evt =>
        {
            Debug.LogFormat("Got a button.{0}", evt.target.ToString());
        });

        mainMenu = root.Q<VisualElement>("main-menu");
        
        settingsUI = root.Q<IMGUIContainer>("settings-menu");
        // settingsUI.SetEnabled(false);
        settingsUI.style.display = DisplayStyle.None; // /DisplayStyle.Flex 
        
        label = root.Q<Label>("main-title");
        label.text = Application.productName;

        // playerStateImage = root.Q<VisualElement>("PlayerStateImage");
        Debug.Log("Attaching Handlers.");

        Button startButton = root.Q<Button>("start-button");
        startButton.clickable = new Clickable(() =>
        {
            SceneManager.LoadScene("AppScene");
        });

        Button settingsButton = root.Q<Button>("settings-button");
        settingsButton.clickable = new Clickable((EventBase evt) =>
        {
            Debug.Log(evt.ToString());
            Debug.Log(evt.imguiEvent.ToString());
            Debug.Log(evt.target.ToString());
            Debug.Log(evt.currentTarget.ToString());
            settingsUI.style.display = DisplayStyle.Flex;
            mainMenu.style.display = DisplayStyle.None;
        });
        root.Q<Button>("back-button").clickable = new Clickable(() =>
        {
            settingsUI.style.display = DisplayStyle.None;
            mainMenu.style.display = DisplayStyle.Flex;
            
        });

        Button quitButton = root.Q<Button>("quit-button");
        quitButton.clickable = new Clickable((evt) =>
        {
            Debug.Log(evt.ToString());
            Debug.Log("Quit Button Clicked.");
#if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif            
        });

    }

    
    private void Update() {
        // rage.value = PlayerController.Rage;
        //
        // if (rage.value == 1) {
        //     playerStateImage.RemoveFromClassList("normal_player_state");
        //     playerStateImage.AddToClassList("rage_player_state");
        // } else {
        //     playerStateImage.AddToClassList("normal_player_state");
        //     playerStateImage.RemoveFromClassList("rage_player_state");
        // }
    }
}
