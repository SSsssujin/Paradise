namespace Paradise
{
    public enum UnitType
    {
        None = -1,
        Basic,
        Elite
    }

    public enum StarRank
    {
        One,
        Two,
        Three,
    }
    
    public enum Element
    {
        None = -1,
        Fire,
        Water,
        Grass,
        
        Light,
        Dark,
        
        // 비행 Flight,
        // 전기 Electric,
        // 바위 Stone,
        // 얼음 Ice,
        // 노말 Normal,
        // 물리 Physical,
    }

    public enum InGameSpeed
    {
        Normal,
        Half,
        Double
    }
    
    public enum UIEvent
    {
        Click,
        Pressed,
        PointerDown,
        PointerUp,
        BeginDrag,
        Drag,
        EndDrag,
    }

    public enum TileState
    {
        Empty, 
        Touched, 
        Occupied
    }


    public struct TileName
    {
        public const string Ground = "Ground";
        public const string Road = "Road";
        public const string Obstacle = "Obstacle";
        public const string Gold = "Gold";
        public const string Sliver = "Sliver";
    }

    public struct MaxCount
    {
        public const int Elite = 3;
        public const int Basic = 5;
    }
}