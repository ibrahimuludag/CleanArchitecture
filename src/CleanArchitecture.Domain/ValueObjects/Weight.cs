namespace CleanArchitecture.Domain.ValueObjects;

public record Weight(MassUnits Unit, float Amount) {
    public static Weight CreateEmpty() => new Weight(MassUnits.None, 0);
}
