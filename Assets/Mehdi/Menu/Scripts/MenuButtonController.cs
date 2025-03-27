// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class MenuButtonController : MonoBehaviour {

// 	// Use this for initialization
// 	public int index;
// 	[SerializeField] bool keyDown;
// 	[SerializeField] int maxIndex;
// 	public AudioSource audioSource;

// 	void Start () {
// 		audioSource = GetComponent<AudioSource>();
// 	}
	
// 	// Update is called once per frame
// 	void Update () {
// 		if(Input.GetAxis ("Vertical") != 0){
// 			if(!keyDown){
// 				if (Input.GetAxis ("Vertical") < 0) {
// 					if(index < maxIndex){
// 						index++;
// 					}else{
// 						index = 0;
// 					}
// 				} else if(Input.GetAxis ("Vertical") > 0){
// 					if(index > 0){
// 						index --; 
// 					}else{
// 						index = maxIndex;
// 					}
// 				}
// 				keyDown = true;
// 			}
// 		}else{
// 			keyDown = false;
// 		}
// 	}

// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour 
{
    public int index = 0; // Indice de l'option sélectionnée
    [SerializeField] bool keyDown;
    [SerializeField] int maxIndex = 3; // Nombre d'options du menu
    public AudioSource audioSource;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() 
    {
        HandleMenuNavigation();
        HandleSelection();
    }

    // Gestion du déplacement dans le menu
    void HandleMenuNavigation()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            if (!keyDown)
            {
                if (Input.GetAxis("Vertical") < 0) 
                {
                    if (index < maxIndex)
                        index++;
                    else
                        index = 0; // Retour au début
                } 
                else if (Input.GetAxis("Vertical") > 0)
                {
                    if (index > 0)
                        index--; 
                    else
                        index = maxIndex; // Aller à la dernière option
                }

                keyDown = true;
                PlaySelectionSound(); // Optionnel : jouer un son
            }
        }
        else
        {
            keyDown = false;
        }
    }

    // Gestion de la sélection avec Entrée
    void HandleSelection()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (index)
            {
                case 0:
                    StartGame();
                    break;
                case 1:
                    OpenSettings();
                    break;
                case 2:
                    ShowCredits();
                    break;
                case 3:
                    QuitGame();
                    break;
                default:
                    Debug.Log("Option non reconnue.");
                    break;
            }
        }
    }

    // Lancer le jeu
    void StartGame()
    {
        Debug.Log("Lancement du jeu...");
        // Charger une scène par exemple : SceneManager.LoadScene("NomDeLaScene");
    }

    // Ouvrir les paramètres
    void OpenSettings()
    {
        Debug.Log("Ouverture des paramètres...");
        // Afficher un menu des options
    }

    // Afficher les crédits
    void ShowCredits()
    {
        Debug.Log("Affichage des crédits...");
        // Afficher une scène de crédits
    }

    // Quitter le jeu
    void QuitGame()
    {
        Debug.Log("Quitter le jeu..."); // Vérification dans la console
        Application.Quit(); // Quitter l'application

        // Dans l'éditeur Unity, Application.Quit() ne fonctionne pas,
        // donc on peut utiliser ceci pour tester :
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    // Optionnel : Jouer un son lorsqu'on change d'option
    void PlaySelectionSound()
    {
        if (audioSource != null)
            audioSource.Play();
    }
}
