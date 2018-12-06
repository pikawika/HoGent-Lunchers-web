export class Aantal {
    private _aantalHandelaars: Number;
    private _aantalLunches: Number;
    private _aantalReservaties: Number;

    static fromJSON(json: any): Aantal {
        if (json != null) {
            let aantal = new Aantal();
            aantal.aantalHandelaars = json.aantalHandelaars;
            aantal.aantalLunches = json.aantalLunches;
            aantal.aantalReservaties = json.aantalReservaties;
            return aantal;
        }
    }

    public get aantalHandelaars(): Number {
        return this._aantalHandelaars;
    }
    public set aantalHandelaars(value: Number) {
        this._aantalHandelaars = value;
    }

    public get aantalLunches(): Number {
        return this._aantalLunches;
    }
    public set aantalLunches(value: Number) {
        this._aantalLunches = value;
    }

    public get aantalReservaties(): Number {
        return this._aantalReservaties;
    }
    public set aantalReservaties(value: Number) {
        this._aantalReservaties = value;
    }
}