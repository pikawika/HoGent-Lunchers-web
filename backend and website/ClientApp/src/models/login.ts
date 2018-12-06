export class Login {
    private _geactiveerd: Boolean;

    static fromJSON(json: any): Login{

        if (json != null) {
            let login = new Login();
            login.geactiveerd = json.geactiveerd;
            return login
        }
    }

    public get geactiveerd(): Boolean {
        return this._geactiveerd;
    }
    public set geactiveerd(value: Boolean) {
        this._geactiveerd = value;
    }
}