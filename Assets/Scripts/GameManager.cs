using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject board;

    public GameObject whiteChip;
    public GameObject blackChip;

    private GameObject[,] pieces;
    private List<GameObject> movedPawns;

    private Player white;
    private Player black;
    public Player currentPlayer;
    public Player otherPlayer;

    public Material defaultMaterial;
    public Material blackMaterial;
    public Material whiteMaterial;
    public Material selectedMaterial;

    void Awake()
    {
        instance = this;
    }

    void Start ()
    {
        pieces = new GameObject[8, 8];
        movedPawns = new List<GameObject>();

        white = new Player("white", true);
        black = new Player("black", false);

        currentPlayer = white;
        otherPlayer = black;

        InitialSetup();
    }

    private void InitialSetup()
    {
        for (int i = 0; i < 8; i+=2)
        {
            AddPiece(whiteChip, white, i, 1);
        }

        for (int i = 1; i < 8; i += 2)
        {
            AddPiece(whiteChip, white, i, 0);
            AddPiece(whiteChip, white, i, 2);
        }

        for (int i = 1; i < 8; i+=2)
        {
            AddPiece(blackChip, black, i, 6);
        }

        for (int i = 0; i < 8; i += 2)
        {
            AddPiece(blackChip, black, i, 5);
            AddPiece(blackChip, black, i, 7);
        }
    }

    public void AddPiece(GameObject prefab, Player player, int col, int row)
    {
        GameObject pieceObject = AddPiece(prefab, col, row);
        player.pieces.Add(pieceObject);
        pieces[col, row] = pieceObject;
    }

    public void SelectPieceAtGrid(Vector2Int gridPoint)
    {
        GameObject selectedPiece = pieces[gridPoint.x, gridPoint.y];
        if (selectedPiece)
        {
            SelectPiece(selectedPiece);
        }
    }

    public List<Vector2Int> MovesForPiece(GameObject pieceObject)
    {
        Piece piece = pieceObject.GetComponent<Piece>();
        Vector2Int gridPoint = GridForPiece(pieceObject); 
        List<Vector2Int> locations = piece.MoveLocations(gridPoint);

        locations.RemoveAll(gp => gp.x < 0 || gp.x > 7 || gp.y < 0 || gp.y > 7);

        locations.RemoveAll(gp => FriendlyPieceAt(gp));

        return locations;
    }

    public void Move(GameObject piece, Vector2Int gridPoint)
    {
        Piece pieceComponent = piece.GetComponent<Piece>();
        if (pieceComponent.type == PieceType.Pawn && !HasPawnMoved(piece))
        {
            movedPawns.Add(piece);
        }

        Vector2Int startGridPoint = GridForPiece(piece);
        pieces[startGridPoint.x, startGridPoint.y] = null;
        pieces[gridPoint.x, gridPoint.y] = piece;
        MovePiece(piece, gridPoint);
    }

    public void PawnMoved(GameObject pawn)
    {
        movedPawns.Add(pawn);
    }

    public bool HasPawnMoved(GameObject pawn)
    {
        return movedPawns.Contains(pawn);
    }

   public void CapturePieceAt(Vector2Int gridPoint)
    {
        GameObject pieceToCapture = PieceAtGrid(gridPoint);
       
        currentPlayer.capturedPieces.Add(pieceToCapture);
        pieces[gridPoint.x, gridPoint.y] = null;
        Destroy(pieceToCapture);
    }
   
    public bool DoesPieceBelongToCurrentPlayer(GameObject piece)
    {
        return currentPlayer.pieces.Contains(piece);
    }

    public GameObject PieceAtGrid(Vector2Int gridPoint)
    {
        if (gridPoint.x > 7 || gridPoint.y > 7 || gridPoint.x < 0 || gridPoint.y < 0)
        {
            return null;
        }
        return pieces[gridPoint.x, gridPoint.y];
    }

    public void WinConditions(Vector2Int gridPoint)
    {
        if((whiteChip)&&(gridPoint.y==7))
        {
            Debug.Log("White wins!");
            Destroy(board.GetComponent<TileSelector>());
            Destroy(board.GetComponent<MoveSelector>());
        }
        else if((blackChip) && (gridPoint.y == 0))
        {
            Debug.Log(currentPlayer.name + "Black wins!");
            Destroy(board.GetComponent<TileSelector>());
            Destroy(board.GetComponent<MoveSelector>());
        }
        pieces[gridPoint.x, gridPoint.y] = null;
        
    }

    public Vector2Int GridForPiece(GameObject piece)
    {
        for (int i = 0; i < 8; i++) 
        {
            for (int j = 0; j < 8; j++)
            {
                if (pieces[i, j] == piece)
                {
                    return new Vector2Int(i, j);
                }
            }
        }

        return new Vector2Int(-1, -1);
    }

    public bool FriendlyPieceAt(Vector2Int gridPoint)
    {
        GameObject piece = PieceAtGrid(gridPoint);

        if (piece == null) {
            return false;
        }

        if (otherPlayer.pieces.Contains(piece))
        {
            return false;
        }

        return true;
    }

    public void NextPlayer()
    {
        Player tempPlayer = currentPlayer;
        currentPlayer = otherPlayer;
        otherPlayer = tempPlayer;
    }
    public GameObject AddPiece(GameObject piece, int col, int row)
    {
        Vector2Int gridPoint = Geometry.GridPoint(col, row);
        GameObject newPiece = Instantiate(piece, Geometry.PointFromGrid(gridPoint), Quaternion.identity, gameObject.transform);
        return newPiece;
    }

    public void RemovePiece(GameObject piece)
    {
        Destroy(piece);
    }

    public void MovePiece(GameObject piece, Vector2Int gridPoint)
    {
        piece.transform.position = Geometry.PointFromGrid(gridPoint);
    }

    public void SelectPiece(GameObject piece)
    {
        MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();
        renderers.material = selectedMaterial;
    }

    public void DeselectPiece(GameObject piece)
    {
        MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();
        
        if (currentPlayer == white)
        {
            renderers.material = whiteMaterial;
        }
        else
        {
            renderers.material = blackMaterial;
        }
    }

}
