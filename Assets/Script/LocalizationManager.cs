using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    public enum Language { Portuguese, English }
    public Language currentLanguage = Language.English;

    private Dictionary<string, string> localizedText = new Dictionary<string, string>();

    [SerializeField] private TextAsset localizationCSV;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLocalization();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void LoadLocalization()
    {
        localizedText.Clear();

        using (StringReader reader = new StringReader(localizationCSV.text))
        {
            string headerLine = reader.ReadLine();
            string[] headers = ParseCSVLine(headerLine);

            int langColumn = currentLanguage == Language.English
                ? System.Array.IndexOf(headers, "en")
                : System.Array.IndexOf(headers, "pt");

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] entries = ParseCSVLine(line);
                if (entries.Length <= langColumn) continue;

                string key = entries[0].Trim();
                string value = entries[langColumn].Trim().Replace("\\n", "\n");

                localizedText[key] = value;
            }
        }
    }

    // Lê uma linha CSV corretamente com suporte a vírgulas dentro de aspas
    string[] ParseCSVLine(string line)
    {
        List<string> result = new List<string>();
        bool inQuotes = false;
        string current = "";

        foreach (char c in line)
        {
            if (c == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(current);
                current = "";
            }
            else
            {
                current += c;
            }
        }
        result.Add(current); // último campo
        return result.ToArray();
    }

    public string Get(string key)
    {
        return localizedText.ContainsKey(key) ? localizedText[key] : $"[{key}]";
    }

    public void SetLanguage(Language lang)
    {
        currentLanguage = lang;
        LoadLocalization();
    }
}
