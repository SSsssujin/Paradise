namespace Paradise
{
    public enum UnitType
    {
        Basic,
        Hero
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

    public struct TileName
    {
        public const string Ground = "Ground";
        public const string Road = "Road";
        public const string Obstacle = "Obstacle";
        public const string Gold = "Gold";
        public const string Sliver = "Sliver";
    }
}