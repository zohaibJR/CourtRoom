using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Case_New", menuName = "Game/Case Data")]
public class CaseData : ScriptableObject
{
    public enum Verdict { Guilty, NotGuilty }

    [Header("Case Info")]
    public string caseName;

    [TextArea(2, 4)]
    public string caseDescription;

    public Verdict correctVerdict;

    [Header("People Involved")]
    public List<CharacterInfo> accusedList;
    public List<CharacterInfo> accuserList;
    public List<CharacterInfo> witnessList;

    [Header("Evidence")]
    public List<EvidenceData> evidence;
}