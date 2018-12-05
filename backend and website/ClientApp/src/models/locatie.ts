export class Locatie {

    private _locatieId: Number;
    private _straat: string;
    private _huisnummer: string;
    private _postcode: string;
    private _gemeente: string;
    private _latitude: Number;
    private _longitude: Number;

    static fromJSON(json: any): Locatie {
        if (json != null) {
            let locatie = new Locatie();

            locatie.locatieId = json.locatieId;
            locatie.straat = json.straat;
            locatie.huisnummer = json.huisnummer;
            locatie.postcode = json.postcode;
            locatie.gemeente = json.gemeente;
            locatie.latitude = json.latitude;
            locatie.longitude = json.longitude
            return locatie;
        }
    }

    /**
     * Getter locatieId
     * @return {Number}
     */
	public get locatieId(): Number {
		return this._locatieId;
	}

    /**
     * Getter straat
     * @return {string}
     */
	public get straat(): string {
		return this._straat;
	}

    /**
     * Getter huisnummer
     * @return {string}
     */
	public get huisnummer(): string {
		return this._huisnummer;
	}

    /**
     * Getter postcode
     * @return {string}
     */
	public get postcode(): string {
		return this._postcode;
	}

    /**
     * Getter gemeente
     * @return {string}
     */
	public get gemeente(): string {
		return this._gemeente;
	}

    /**
     * Getter latitude
     * @return {Number}
     */
	public get latitude(): Number {
		return this._latitude;
	}

    /**
     * Getter longitude
     * @return {Number}
     */
	public get longitude(): Number {
		return this._longitude;
	}

    /**
     * Setter locatieId
     * @param {Number} value
     */
	public set locatieId(value: Number) {
		this._locatieId = value;
	}

    /**
     * Setter straat
     * @param {string} value
     */
	public set straat(value: string) {
		this._straat = value;
	}

    /**
     * Setter huisnummer
     * @param {string} value
     */
	public set huisnummer(value: string) {
		this._huisnummer = value;
	}

    /**
     * Setter postcode
     * @param {string} value
     */
	public set postcode(value: string) {
		this._postcode = value;
	}

    /**
     * Setter gemeente
     * @param {string} value
     */
	public set gemeente(value: string) {
		this._gemeente = value;
	}

    /**
     * Setter latitude
     * @param {Number} value
     */
	public set latitude(value: Number) {
		this._latitude = value;
	}

    /**
     * Setter longitude
     * @param {Number} value
     */
	public set longitude(value: Number) {
		this._longitude = value;
	}
}