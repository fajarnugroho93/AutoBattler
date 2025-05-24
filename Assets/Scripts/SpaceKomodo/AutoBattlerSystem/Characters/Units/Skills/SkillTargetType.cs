namespace SpaceKomodo.AutoBattlerSystem.Characters.Units.Skills
{
    public enum SkillTargetType
    {
        None = 0,
        
        Ally = 1,
        Enemy = 2,
        
        Alive = 100,
        Dead = 101,
        
        ClosestColumn = 200,
        FarthestColumn = 201,
        
        SameRow = 300,
    }
}