export class Klant {

    private _gebruikerId: Number;
    private _telefoonnummer: string;
    private _email: string;
    private _voornaam: string;
    private _achternaam: string;

    static fromJSON(json: any): Klant {
        if (json != null) {
            let klant = new Klant();

            klant.gebruikerId = json.gebruikerId;
            klant.telefoonnummer = json.telefoonnummer;
            klant.email = json.email;
            klant.voornaam = json.voornaam;
            klant.achternaam = json.achternaam;

            return klant;
        }
    }

    /**
     * Getter gebruikerId
     * @return {Number}
     */
	public get gebruikerId(): Number {
		return this._gebruikerId;
	}

    /**
     * Getter telefoonnummer
     * @return {string}
     */
	public get telefoonnummer(): string {
		return this._telefoonnummer;
	}

    /**
     * Getter email
     * @return {string}
     */
	public get email(): string {
		return this._email;
	}

    /**
     * Getter voornaam
     * @return {string}
     */
	public get voornaam(): string {
		return this._voornaam;
	}

    /**
     * Getter achternaam
     * @return {string}
     */
	public get achternaam(): string {
		return this._achternaam;
	}

    /**
     * Setter gebruikerId
     * @param {Number} value
     */
	public set gebruikerId(value: Number) {
		this._gebruikerId = value;
	}

    /**
     * Setter telefoonnummer
     * @param {string} value
     */
	public set telefoonnummer(value: string) {
		this._telefoonnummer = value;
	}

    /**
     * Setter email
     * @param {string} value
     */
	public set email(value: string) {
		this._email = value;
	}

    /**
     * Setter voornaam
     * @param {string} value
     */
	public set voornaam(value: string) {
		this._voornaam = value;
	}

    /**
     * Setter achternaam
     * @param {string} value
     */
	public set achternaam(value: string) {
		this._achternaam = value;
	}
}
