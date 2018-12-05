import { Lunch } from "./lunch";
import { Klant } from "./klant";
import { Handelaar } from "./handelaar";

export class Reservatie {

    private _reservatieId: Number;
    private _lunch: Lunch;
    private _aantal: Number;
    private _datum: Date;
    private _klant: Klant;
    private _handelaar: Handelaar;
    private _status: Status;

    static fromJSON(json: any): Reservatie {
        if (json != null) {
            let reservatie = new Reservatie();

            reservatie.reservatieId = json.reservatieId;
            reservatie.lunch = json.lunch;
            reservatie.aantal = json.aantal;
            reservatie.datum = json.datum;
            reservatie.klant = Klant.fromJSON(json.klant);
            reservatie.handelaar = Handelaar.fromJSON(json.lunch.handelaar);      

            switch (Number.parseInt(json.status)) {
                case 0: {
                    reservatie.status = Status.InAfwachting;
                    break;
                }
                case 1: {
                    reservatie.status = Status.Goedgekeurd;
                    break;
                }
                case 2: {
                    reservatie.status = Status.Afgekeurd;
                    break;
                }
                default: {
                    reservatie.status = Status.Onbekend;
                    break;
                }
            }

            return reservatie;
        }
    }

    /**
     * Getter reservatieId
     * @return {Number}
     */
    public get reservatieId(): Number {
        return this._reservatieId;
    }

    /**
     * Getter lunch
     * @return {Lunch}
     */
    public get lunch(): Lunch {
        return this._lunch;
    }

    /**
     * Getter aantal
     * @return {Number}
     */
    public get aantal(): Number {
        return this._aantal;
    }

    /**
     * Getter datum
     * @return {Date}
     */
    public get datum(): Date {
        return this._datum;
    }

    /**
     * Getter klant
     * @return {Klant}
     */
    public get klant(): Klant {
        return this._klant;
    }

    /**
     * Getter status
     * @return {Status}
     */
    public get status(): Status {
        return this._status;
    }

    /**
     * Setter reservatieId
     * @param {Number} value
     */
    public set reservatieId(value: Number) {
        this._reservatieId = value;
    }

    /**
     * Setter lunch
     * @param {Lunch} value
     */
    public set lunch(value: Lunch) {
        this._lunch = value;
    }

    /**
     * Setter aantal
     * @param {Number} value
     */
    public set aantal(value: Number) {
        this._aantal = value;
    }

    /**
     * Setter datum
     * @param {Date} value
     */
    public set datum(value: Date) {
        this._datum = value;
    }

    /**
     * Setter klant
     * @param {Klant} value
     */
    public set klant(value: Klant) {
        this._klant = value;
    }

    /**
     * Setter status
     * @param {Status} value
     */
    public set status(value: Status) {
        this._status = value;
    }

    public get handelaar(): Handelaar {
        return this._handelaar;
    }

    public set handelaar(value: Handelaar) {
        this._handelaar = value;
    }

}

enum Status {
    InAfwachting = "In afwachting",
    Goedgekeurd = "Goedgekeurd",
    Afgekeurd = "Afgekeurd",
    Onbekend = "Onbekend"
}

