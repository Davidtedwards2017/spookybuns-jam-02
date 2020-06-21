using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextTyper : MonoBehaviour
{
    private Text _TextArea;
    private float TimePerCharacter = 0.032f;

    private List<string> Tags;
    int substringLength = 1;

    private void Awake()
    {
        _TextArea = GetComponent<Text>();
        SetText("");
    }

    public IEnumerator PerformTextTyping(string value, UnityAction onTypeCallBack = null)
    {
        Tags = new List<string>();
        SetText(GetWhiteSpaces(value.Length));
        string previous = "";
        int head = 0;
        while(head < value.Length)
        {
            var next = Parse(ref head, value);
            next = ApplyTags(next);

            var spaces = GetWhiteSpaces(value.Length - (head + substringLength));
            SetText(string.Format("{0}{1}{2}", previous, next, spaces));
            if(onTypeCallBack != null)
            {
                onTypeCallBack.Invoke();
            }
            yield return new WaitForSeconds(TimePerCharacter);
            previous += next;
        }

        SetText(value);
    }

    private string Parse(ref int head, string text)
    {
        var value = text.Substring(head++, substringLength);
        switch (value)
        {
            case "<":
                string tag = "";
                bool endTag = true;
                while(value != ">")
                {
                    value = text.Substring(head++, substringLength);
                    if (value == ">")
                    {
                        if(endTag)
                        {
                            Tags.Add(tag);
                        }
                        else
                        {
                            Tags.Remove(tag);
                        }
                        break;
                    }
                    else if (value == "/")
                    {
                        endTag = false;
                    }
                    else
                    {
                        tag += value;
                    }
                }
                return Parse(ref head, text);
            default:
                return value;
        }
    }

    private string ApplyTags(string text)
    {
        foreach (var tag in Tags)
        {
            var open = "<" + tag + ">";
            var close = "</"+ Regex.Match(tag, @"^([\w\-]+)").Value +">";
            text = open + text + close;
        }

        return text;
    }

    private string GetWhiteSpaces(int count)
    {
        string spaces = "";
        while(count-- > 0)
        {
            spaces += " ";
        }

        return spaces;
    }

    private void SetText(string value)
    {
        _TextArea.text = value;
    }
}
