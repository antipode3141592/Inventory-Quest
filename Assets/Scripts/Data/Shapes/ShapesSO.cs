using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Data.Shapes
{
    [CreateAssetMenu(menuName = "InventoryQuest/Items/Shapes", fileName = "shape_")]
    public class ShapesSO : SerializedScriptableObject, IShape
    {
        [SerializeField] private readonly string id;
        [SerializeField] private readonly bool isRotationallySymmetric;
        [SerializeField] private readonly bool isChiral;
        [SerializeField] private readonly int minoCount;
        [SerializeField] private Coor size = new(1,1);


        [SerializeField] bool[,] defaultFacing;

        [SerializeField] Dictionary<Facing, bool[,]> facings = new();


        [Button(Name ="Generate Facings")]
        void GenerateInitialFacings()
        {
            facings = new()
            {
                { Facing.Right, new bool[size.row, size.column] },
                { Facing.Down, new bool[size.column, size.row] },
                { Facing.Left, new bool[size.row, size.column] },
                { Facing.Up, new bool[size.column, size.row] }
            };
            GenerateFacingRotations();
        }

        void GenerateFacingRotations()
        {
            facings[Facing.Right] = defaultFacing;
            for (int i = 1; i < 4; i++)
            {
                facings[(Facing)i] = RotateCC(facings[(Facing)i - 1]);
            }
        }

        bool[,] RotateCC(bool[,] input)
        {
            bool[,] newArray = new bool[input.GetLength(1), input.GetLength(0)];

            for (int r = input.GetLength(0) - 1; r >= 0; r--)
            {
                for (int c = 0; c < input.GetLength(1); c++)
                {
                    newArray[c, input.GetLength(0) - 1 - r] = input[r, c];
                }
            }
            return newArray;
        }

        private Dictionary<Facing, HashSet<Coor>> points = new();

        public string Id => id;
        public bool IsRotationallySymmetric => isRotationallySymmetric;
        public bool IsChiral => isChiral;
        public int MinoCount => minoCount;
        public Dictionary<Facing, HashSet<Coor>> Points => points;

        protected void Start()
        {
            foreach(var face in facings)
            {
                var set = new HashSet<Coor>();
                for(int r = 0; r < face.Value.GetLength(0); r++)
                {
                    for (int c = 0; c < face.Value.GetLength(1); c++)
                    {
                        if (face.Value[r, c])
                            set.Add(new Coor(r, c));
                    }
                }
                points.Add(face.Key, set);
            }
            Debug.Log($"There are {points.Count} point collections with {points[Facing.Right].Count} points each");
        }
    }
}
