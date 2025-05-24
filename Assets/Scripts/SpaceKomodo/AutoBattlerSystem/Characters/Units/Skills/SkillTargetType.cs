namespace SpaceKomodo.AutoBattlerSystem.Characters.Units.Skills
{
    public enum SkillTargetType
    {
        None = 0,
        
        Self = 1,
        Ally = 2,
        Enemy = 3,
        
        Alive = 100,
        Dead = 101,
        
        ClosestColumn = 200,
        FarthestColumn = 201,
        
        ClosestRow = 300,
        FarthestRow = 301,
        SameRow = 302,
    }
}