using UnityEngine; 
using System.Collections.Generic; 
public class Lineup : MonoBehaviour 
{ 
    public static Lineup Instance; 
    public LineupPanel lineupPanel; 
    public List<PositionInLineup> myLineup = new List<PositionInLineup>(); 
    private void Awake() 
    { 
        Instance = this; 
    } 
    // ================= ADD ================= 
    public void AddLineup(int character, Position position) 
    { 
        // Kiểm tra nếu đã có vị trí này trong đội hình, nếu có thì cập nhật lại 
        PositionInLineup existing = myLineup.Find(p => p.position == position); 
        if (existing != null) 
        { 
            existing.CharIndex = character; 
        } 
        else 
        { 
            myLineup.Add(new PositionInLineup { position = position, CharIndex = character }); 
        } 
        GameManager.Instance.UpdateTeamPower();
    } 
    // ================= REMOVE ================= 
    public void RemoveLineup(Position position) 
    { 
        // Tìm và xóa vị trí trong đội hình 
        myLineup.RemoveAll(p => p.position == position);
        GameManager.Instance.UpdateTeamPower();
    } 
    public void RefreshUI()
    {
        lineupPanel.UpdateAllSlots();
    }
} 
public enum Position { Front1, Front2, Front3, Back1, Back2, Back3 } 

[System.Serializable] 
public class PositionInLineup 
{ 
    public Position position; 
    public int CharIndex; // index trong inventory 
}