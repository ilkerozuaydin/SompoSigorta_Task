

var AJAX_ENDPOINTS = {
    Proposal: {
        GetProposals: "/Home/GetProposal",
        RenderProposalTable:"/Home/GetProposalTable"
    }
}

var CONTENT_TYPES = {
    JSON: "application/json",
};
var DATA_TYPES = {
    JSON: "json",
    HTML:"html"
};
var SUCCESS_TYPES = {
    NOTIFY: 1,
    DONOTING: 2
};

var PROPOSAL_STATUS_TYPES = {
    POSITIVE: 1,
    INFORMATION: 2,
    NEGATIVE:3
}