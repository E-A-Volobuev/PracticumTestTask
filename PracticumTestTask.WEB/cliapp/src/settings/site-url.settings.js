const SiteUrl = {

    getStatistics: () => `https://localhost:44393/Action/Process?url=`,
    getHistoryRequest:() => `https://localhost:44393/Action/GetAllRequests`,
    getResults:() => `https://localhost:44393/Action/GetRequestUrl?id=`,
    delete:() => `https://localhost:44393/Action/DeleteRequest?id=`,

};

export default SiteUrl;