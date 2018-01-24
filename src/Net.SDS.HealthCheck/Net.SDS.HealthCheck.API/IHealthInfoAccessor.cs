namespace Service.A
{
    internal interface IHealthInfoAccessor
    {
        HealthInfo GetHealthInfo(HealthFlags flag);
    }
}
