import { Lunch } from "./lunch";
import { Klant } from "./klant";

export class Reservatie {

    private _reservatieId: Number;
    private _lunch: Lunch;
    private _aantal: Number;
    private _datum: Date;
    private _klant: Klant;
    private _status: Status;

    static fromJSON(json: any): Reservatie {
        if (json != null) {
            let reservatie = new Reservatie();

            reservatie.reservatieId = json.gebruikderId;
            reservatie.lunch = json.telefoonnummer;
            reservatie.aantal = json.email;
            reservatie.datum = json.email;
            reservatie.klant = json.voornaam;
            reservatie.status = this.giveStatus(json.status);

            return reservatie;
        }
    }

    static giveStatus(value: Number): Status {
        switch(value) {
            case 0: {
                return Status.InAfwachting;
            }
            case 1: {
                return Status.Goedgekeurd;
            }
            case 2: {
                return Status.Afgekeurd;
            }
            default: {
                return Status.InAfwachting;
            }
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
    
}

enum Status {
    InAfwachting,
    Goedgekeurd,
    Afgekeurd,
}

