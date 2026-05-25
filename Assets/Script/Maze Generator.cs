using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;
using Unity.AI.Navigation;


public class MazeGenerator : MonoBehaviour
{

    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GameObject[] mirrorCellPrefabs;
    [SerializeField] private int width, depth, startX, startZ;
    [SerializeField] private float sizeMultiplier;
    [SerializeField, Range(0, 10)] private int mirrorCellRatio;
    public NavMeshSurface navMesh;
    private MazeCell[,] mazeGrid;

    void Start()
    {
        mazeGrid = new MazeCell[width, depth];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < depth; j++)
            {
                if (Random.Range(0, 10) > mirrorCellRatio)
                {
                    mazeGrid[i, j] = Instantiate(cellPrefab, new Vector3(startX + i * sizeMultiplier, 0, startZ + j * sizeMultiplier), Quaternion.identity).GetComponent<MazeCell>();
                }
                else
                {
                    mazeGrid[i, j] = Instantiate(mirrorCellPrefabs[Random.Range(0, mirrorCellPrefabs.Length)], new Vector3(startX + i * sizeMultiplier, 0, startZ + j * sizeMultiplier), Quaternion.identity).GetComponent<MazeCell>();
                }
            }
        }

        GenMaze(null, mazeGrid[0,0]);
    }
    private void GenMaze(MazeCell prevCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(prevCell, currentCell);

        //yield return new WaitForSeconds(0.1f);

        MazeCell nextCell = NextCell(currentCell);
        while (nextCell != null)
        {

            if (nextCell != null)
            GenMaze(currentCell, nextCell);
            nextCell = NextCell(currentCell);

        }
        navMesh.RemoveData();
        navMesh.BuildNavMesh();

    }

    private MazeCell NextCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell).ToList();

        if (unvisitedCells.Count == 0)
            return null;

        return unvisitedCells[Random.Range(0, unvisitedCells.Count)];
    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = (int)((currentCell.transform.position.x-startX) / sizeMultiplier);
        int z = (int)((currentCell.transform.position.z-startZ) / sizeMultiplier);

        if (x+1 < width)
        {
            var checkCell = mazeGrid[x+1, z];

            if (checkCell.IsVisited != true)
            {
                yield return checkCell;
            }
        }
        if (x-1 >= 0)
        {
            var checkCell = mazeGrid[x-1, z];

            if (checkCell.IsVisited != true)
            {
                yield return checkCell;
            }
        }
        if (z-1 >= 0)
        {
            var checkCell = mazeGrid[x, z-1];

            if (checkCell.IsVisited != true)
            {
                yield return checkCell;
            }
        }
        if (z+1 < depth)
        {
            var checkCell = mazeGrid[x, z+1];

            if (checkCell.IsVisited != true)
            {
                yield return checkCell;
            }
        }
    }

    private void ClearWalls(MazeCell prevCell, MazeCell currentCell)
    {
        if (prevCell == null)
            return;

        if (prevCell.gameObject.transform.position.x < currentCell.gameObject.transform.position.x)
        {
            prevCell.ClearWall(1);
            currentCell.ClearWall(0);
            return;
        }
        if (prevCell.gameObject.transform.position.x > currentCell.gameObject.transform.position.x)
        {
            prevCell.ClearWall(0);
            currentCell.ClearWall(1);
            return;
        }
        if (prevCell.gameObject.transform.position.z < currentCell.gameObject.transform.position.z)
        {
            prevCell.ClearWall(2);
            currentCell.ClearWall(3);
            return;
        }
        if (prevCell.gameObject.transform.position.z > currentCell.gameObject.transform.position.z)
        {
            prevCell.ClearWall(3);
            currentCell.ClearWall(2);
            return;
        }



    }

}
