namespace ForkToFit.Repositories
{
    public class MealPlan
    {
        public int Id { get; internal set; }
        public string Name { get; internal set; }
        public int UserProfileId { get; internal set; }
        public int MealPlanTypeId { get; internal set; }
        public string CalorieTracker { get; internal set; }
    }
}