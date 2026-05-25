using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [SerializeField] private GameObject[] walls;
    [SerializeField] private GameObject unvisited;
    public bool IsVisited { get; private set; }

    public void Visit()
    {
        IsVisited = true;
        unvisited.SetActive(false);
    }

    public void ClearWall(int id)
    {
        walls[id].gameObject.SetActive(false);
    }
    //public void ClearLeftWall()
    //{
    //    walls[0].gameObject.SetActive(false);
    //}
    //public void ClearRightWall()
    //{
    //    walls[1].gameObject.SetActive(false);
    //}
    //public void ClearFrontWall()
    //{
    //    walls[2].gameObject.SetActive(false);
    //}
    //public void ClearBackWall()
    //{
    //    walls[3].gameObject.SetActive(false);
    //}
}
