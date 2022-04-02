const SiteUrl = {

    getStatistics: () => `https://localhost:44333/Action/Process?url=`,
    getHistoryRequest:() => `https://localhost:44333/Action/GetAllRequests`,
    getResults:() => `https://localhost:44333/Action/GetRequestUrl?id=`,
    delete:() => `https://localhost:44333/Action/DeleteRequest?id=`,

};

export default SiteUrl;