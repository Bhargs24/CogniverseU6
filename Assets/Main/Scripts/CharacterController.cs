using UnityEngine;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;

    [System.Serializable]
    public class Character
    {
        public string characterID;
        public GameObject characterPrefab;
        public Transform spawnPoint;
        public Animator animator;
        public AudioSource audioSource;
    }

    public List<Character> characters = new List<Character>();

    private Dictionary<string, Character> characterDictionary = new Dictionary<string, Character>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeCharacters();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeCharacters()
    {
        foreach (var character in characters)
        {
            characterDictionary.Add(character.characterID, character);
        }
    }

    public GameObject SpawnCharacter(string characterID, Vector3 position, Quaternion rotation)
    {
        if (characterDictionary.TryGetValue(characterID, out Character character))
        {
            GameObject instance = Instantiate(character.characterPrefab, position, rotation);
            character.animator = instance.GetComponent<Animator>();
            character.audioSource = instance.GetComponent<AudioSource>();
            return instance;
        }
        else
        {
            Debug.LogWarning($"Character with ID {characterID} not found!");
            return null;
        }
    }

    public void RelocateCharacter(string characterID, Vector3 position, Quaternion rotation)
    {
        if (characterDictionary.TryGetValue(characterID, out Character character))
        {
            character.spawnPoint.position = position;
            character.spawnPoint.rotation = rotation;
        }
        else
        {
            Debug.LogWarning($"Character with ID {characterID} not found!");
        }
    }

    public void PlayAnimation(string characterID, string animationTrigger)
    {
        if (characterDictionary.TryGetValue(characterID, out Character character) && character.animator != null)
        {
            character.animator.SetTrigger(animationTrigger);
        }
    }

    public void PlayAudio(string characterID, AudioClip clip)
    {
        if (characterDictionary.TryGetValue(characterID, out Character character) && character.audioSource != null)
        {
            character.audioSource.clip = clip;
            character.audioSource.Play();
        }
    }
}
