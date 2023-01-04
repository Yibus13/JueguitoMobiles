﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Match3GameManager : MonoBehaviour
{

    Tile[,] Board;

    [SerializeField]
    int sizeX;
    public int sizeY;

    [SerializeField]
    Tile[] Squares;
    [SerializeField]
    Box[] Heads;

    bool movement = false;

    int dragX, dragY = -1;

    [SerializeField]
    int CarolPoints;
    [SerializeField]
    int NorbertPoints;
    [SerializeField]
    int G470Points;
    [SerializeField]
    int NASSPoints;
    [SerializeField]
    int DeclanPoints;
    [SerializeField]
    int WinniePoints;

    [SerializeField]
    Box CarolHead;
    Box NorbertHead;
    Box G470Head;
    [SerializeField]
    Box NASSHead;
    [SerializeField]
    Box DeclanHead;
    [SerializeField]
    Box WinnieHead;


    void Start()
    {
        print(DeclanPoints + "ID " + WinniePoints + "IW " + NASSPoints + "IN " + CarolPoints + "IC ");

        Board = new Tile[sizeX, sizeY * 2];
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                CreateTile(i, j);
            }
        }

        foreach (Box b in Heads)//Creación de las cabezas
        {
            b.Points = 0;
            int x = 0;
            int y = 0;
            do
            {
                x = Random.Range(0, sizeX);
                y = Random.Range(0, sizeY);
            } while (!NotNeighbour(x, y, b));
            CreateHead(x, y, b);
            switch (b.type)
            {
                case "Naranja":
                    CarolHead = b;
                    break;
                case "Azul":
                    NASSHead = b;
                    break;
                case "Rosa":
                    WinnieHead = b;
                    break;
                case "Rojo":
                    DeclanHead = b;
                    break;
                case "AzulC":
                    G470Head = b;
                    break;
                case "Marron":
                    NorbertHead = b;
                    break;
            }
        }

        //print(DeclanHead.Points + "IDH " + WinnieHead.Points + "IWH " + NASSHead.Points + "INH " + CarolHead.Points + "ICH ");

        DestroySquares();
    }

    bool NotNeighbour(int x1, int y1, Tile t)
    {
        if (x1 - 1 < 0 || Board[x1 - 1, y1] == null || Board[x1 - 1, y1].type == t.type) return false;
        if (x1 + 1 >= sizeX || Board[x1 + 1, y1] == null || Board[x1 + 1, y1].type == t.type) return false;
        if (y1 - 1 < 0 || Board[x1, y1 - 1] == null || Board[x1, y1 - 1].type == t.type) return false;
        if (y1 + 1 >= sizeY || Board[x1, y1 + 1] == null || Board[x1, y1 + 1].type == t.type) return false;
        return true;
    }

    public void Drag(Tile square)//cuando se haga clic a la primera caja se guardaran sus valores para comprobarlos
    {
        if (!movement) return;
        dragX = square.x;
        dragY = square.y;
    }

    public void Drop(Tile square)//cuando se suelte el ratos se verán los valores de la casilla en la que se han soltado si se cumplen las condiciones se realiza el cambio
    {
        if (!movement) return;
        if (dragX == -1 || dragY == -1) return;
        if (Neighbour(dragX, dragY, square.x, square.y)) return;
        SwapBoxes(dragX, dragY, square.x, square.y);
    }
    bool Neighbour(int x1, int y1, int x2, int y2)//comprobar que son casillas vecinas
    {
        if ((x1 + 1 == x2 || x1 - 1 == x2 || x1 == x2) && (y1 - 1 == y2 || y1 + 1 == y2 || y1 == y2))
        {
            return false;
        }
        return true;
    }

    void SwapBoxes(int x1, int y1, int x2, int y2)//cambio de las casillas y comporbar combinaciones etc
    {
        MoveTile(x1, y1, x2, y2);

        dragX = -1;//resetear los valores
        dragY = -1;

        List<Tile> DestroyArray = Horizontal();
        DestroyArray.AddRange(Vertical());

        if (DestroyArray.Count == 0) MoveTile(x1, y1, x2, y2);//si no hay casillas a eliminar se vuelven a cambiar las posiciones

        DestroySquares();
    }
    void DestroySquares()
    {
        List<Tile> SquaresEliminated = Horizontal();
        SquaresEliminated.AddRange(Vertical());

        SquaresEliminated = SquaresEliminated.Distinct().ToList();

        addPoints(SquaresEliminated);

        bool sw = SquaresEliminated.Count == 0;

        foreach (Tile t in SquaresEliminated)
        {
            if (t != null)
            {
                if (t.GetComponent<Box>())
                {
                    int Xposition = Random.Range(0, sizeX);
                    CreateTile(t.x, t.y + sizeY);
                    CreateHead(Xposition, sizeY, t.GetComponent<Box>());
                    Destroy(t.gameObject);
                }
                else
                {
                    Destroy(t.gameObject);
                    CreateTile(t.x, t.y + sizeY);
                }
            }
        }

        if (!sw)
            StartCoroutine(BoardUpdate());
    }

    void addPoints(List<Tile> Squares)
    {
        foreach (Tile t in Squares)
        {
            switch (t.type)
            {
                case "Naranja":
                    CarolHead.Points++;
                    break;
                case "Azul":
                    NASSHead.Points++;
                    break;
                case "Rosa":
                    WinnieHead.Points++;
                    break;
                case "Rojo":
                    DeclanHead.Points++;
                    break;
                case "Marron":
                    NorbertHead.Points++;
                    break;
                case "AzulC":
                    G470Head.Points++;
                    break;
            }
            if (t.GetComponent<Box>())
            {
                switch (t.type)
                {
                    case "Amarillo":
                        CarolPoints += CarolHead.Points;
                        CarolHead.Points = 0;
                        break;
                    case "Azul":
                        NASSPoints += NASSHead.Points;
                        NASSHead.Points = 0;
                        break;
                    case "Rojo":
                        WinniePoints += WinnieHead.Points;
                        WinnieHead.Points = 0;
                        break;
                    case "Verde":
                        DeclanPoints += DeclanHead.Points;
                        DeclanHead.Points = 0;
                        break;
                }
            }
        }
        print(DeclanPoints + "D " + WinniePoints + "W " + NASSPoints + "N " + CarolPoints + "C ");
        print(DeclanHead.Points + "DH " + WinnieHead.Points + "WH " + NASSHead.Points + "NH " + CarolHead.Points + "CH ");
    }
    IEnumerator BoardUpdate()
    {
        bool Sw = true;
        movement = false;//desactivar movimiento mientras se actualiza el tablero

        while (Sw)//comprobar constantemente que si hay un espacio vacío se caiga el bloque de arriba
        {
            Sw = false;//se resetea todo el rato para el momento en el que no haya mas casillas lo deje de hacer
            for (int j = 0; j < sizeY * 2; j++)
            {
                for (int i = 0; i < sizeX; i++)
                {
                    if (Fall(i, j))
                    {
                        Sw = true;//se pone para seguir comprobando
                    }
                }
                if (j <= sizeY) yield return null;
            }
        }
        movement = true;
        DestroySquares();
    }
    bool Fall(int x, int y)
    {
        if (x < 0 || x >= sizeX || y <= 0 || y >= sizeY * 2 || Board[x, y] == null || Board[x, y - 1] != null) return false;
        MoveTile(x, y, x, y - 1);
        return true;
    }

    List<Tile> Vertical()//funcion para comprobar si verticalmente coinciden las casillas
    {
        List<Tile> Matched = new List<Tile>();
        List<Tile> Aux = new List<Tile>();
        string Type = "";//se establece una variable para guardar el tipo que se esta comprobando

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                if (Type == "") Type = Board[i, j].type;
                if (Board[i, j].type == Type) Aux.Add(Board[i, j]);//si son iguales al tipo se van guardando en un array de auxiliares
                else
                {
                    if (Aux.Count >= 3)//en el momento en el que no sean iguales al tipo se comprueba si el tamaño del array de auxiliares es mayor o igual que 3, si lo es, se ponen en el array para eliminarse
                    {
                        Matched.AddRange(Aux);
                    }
                    Aux.Clear();//se borra el auxiliar para empezar de nuevo
                    Type = Board[i, j].type;
                    Aux.Add(Board[i, j]);//se cambia el tipo y se mete la casilla
                }
            }
            if (Aux.Count >= 3)//cuando se acaba se vuelve a comprobar
            {
                Matched.AddRange(Aux);
            }
            Aux.Clear();
        }
        return Matched;
    }

    List<Tile> Horizontal()//funcion para comprobar si verticalmente coinciden las casillas
    {
        List<Tile> Matched = new List<Tile>();
        List<Tile> Aux = new List<Tile>();
        string Type = "";//se establece una variable para guardar el tipo que se esta comprobando

        for (int j = 0; j < sizeY; j++)
        {
            for (int i = 0; i < sizeX; i++)
            {
                if (Type == "") Type = Board[i, j].type;
                if (Board[i, j].type == Type) Aux.Add(Board[i, j]);//si son iguales al tipo se van guardando en un array de auxiliares
                else
                {
                    if (Aux.Count >= 3)//en el momento en el que no sean iguales al tipo se comprueba si el tamaño del array de auxiliares es mayor o igual que 3, si lo es, se ponen en el array para eliminarse
                    {
                        Matched.AddRange(Aux);
                    }
                    Aux.Clear();//se borra el auxiliar para empezar de nuevo
                    Type = Board[i, j].type;
                    Aux.Add(Board[i, j]);//se cambia el tipo y se mete la casilla
                }
            }
            if (Aux.Count >= 3)//cuando se acaba se vuelve a comprobar
            {
                Matched.AddRange(Aux);
            }
            Aux.Clear();
        }
        return Matched;
    }

    void MoveTile(int x1, int y1, int x2, int y2)
    {
        if (Board[x1, y1] != null) Board[x1, y1].transform.position = new Vector3(x2, y2);//Cambio de posición visible de las casillas por pantalla
        if (Board[x2, y2] != null) Board[x2, y2].transform.position = new Vector3(x1, y1);

        Tile temp = Board[x1, y1];//Cambio en el propio array de las casillas
        Board[x1, y1] = Board[x2, y2];
        Board[x2, y2] = temp;

        if (Board[x1, y1] != null) Board[x1, y1].ChangePosition(x1, y1);//Cambio de la posición interna de las casillas
        if (Board[x2, y2] != null) Board[x2, y2].ChangePosition(x2, y2);
    }
    void CreateTile(int x, int y)
    {
        Tile Square = Instantiate(Squares[Random.Range(0, Squares.Length)], new Vector3(x, y), Quaternion.identity, transform);
        Square.Constructor(this, x, y);
        if (Board[x, y] == null) Board[x, y] = Square;
    }

    void CreateHead(int x, int y, Tile t)
    {
        if (Board[x, y] != null)
        {
            while (Board[x, y] != null && Board[x, y].GetComponent<Box>()) y++;
            if(Board[x, y] != null) Destroy(Board[x, y].gameObject);
        }
        Tile Square = Instantiate(t, new Vector3(x, y), Quaternion.identity, transform);
        Square.Constructor(this, x, y);
        Board[x, y] = Square;
    }
}