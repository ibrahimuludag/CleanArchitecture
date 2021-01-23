export class ClientBase {
    /**
     * authorization token value
     */
    public static token: string | null = null;


    protected transformOptions(options: any) {

        if (ClientBase.token) {
            options.headers["Authorization"] = "Bearer " + ClientBase.token
        } else {
            console.warn("Authorization token have not been set please authorize first.");
        }
        return Promise.resolve(options);
    }
}
