using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MazeVisualizer))]
public class MazeBuilder : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public int seed = -1;

    MazeVisualizer visualizer;
    MazeBase maze;
    private void Awake()
    {
        visualizer = GetComponent<MazeVisualizer>();
    }

    public void Build()
    {
        maze = new WilsonMaze(width, height, seed);
        visualizer.Clear();
        visualizer.Draw(maze);
    }
}
