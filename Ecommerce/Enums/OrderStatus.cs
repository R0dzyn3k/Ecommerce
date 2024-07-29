namespace Ecommerce.Enums;

public enum OrderStatus
{
    /**
     * <p><b>Koszyk</b></p>
     *
     * Używany dla rozróżnienia koszyków od złożonych zamówień
     */
    Draft,

    /**
    * <p><b>Zamówienie złożone</b></p>
    *
    * Nowe zamówienie które zostało dopiero złożone, ale nie zostały jeszcze rozpoczęte żadne procesy
    * dla tego zamówienia.
    */
    New,

    /**
     * <p><b>Przetwarzanie</b></p>
     *
     * Ten status może być użyty aby oznaczyć zamówienie jako przetwarzane.
     */
    Processing,

    /**
     * <p><b>Zakończone</b></p>
     *
     * Zamówienie zostało w pełni zakończone.
     */
    Completed,


    /**
     * <p><b>Anulowane</b></p>
     *
     * Zamówienie zostało anulowane.
     */
    Cancelled
}