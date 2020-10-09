var IndexManager = {

    init: function () {
        $("#btnReport").click(function () {
            var proposalNo = parseInt($("#txtProposalNo").val());
            var endorsNo = parseInt($("#txtEndorsNo").val());
            var renewalNo = parseInt($("#txtRenewalNo").val());
            var productNo = $("#txtProductNo").val();
            var data = {
                ProposalNo: proposalNo,
                EndorsNo: endorsNo,
                RenewalNo: renewalNo,
                ProductNo: productNo
            };
            AjaxFunctionPOST(data, AJAX_ENDPOINTS.Proposal.GetProposals, CONTENT_TYPES.JSON, SUCCESS_TYPES.NOTIFY)().done(function (response) {
                if (response.results) {
                    var positiveResults = {
                        StatusType: PROPOSAL_STATUS_TYPES.POSITIVE,
                        TableData: response.results.filter(function (t) { return t.status.value == PROPOSAL_STATUS_TYPES.POSITIVE })
                    };
                    var informationResults = {
                        StatusType: PROPOSAL_STATUS_TYPES.INFORMATION,
                        TableData: response.results.filter(function (t) { return t.status.value == PROPOSAL_STATUS_TYPES.INFORMATION })
                    };
                    var negativeResults = {
                        StatusType: PROPOSAL_STATUS_TYPES.NEGATIVE,
                        TableData: response.results.filter(function (t) { return t.status.value == PROPOSAL_STATUS_TYPES.NEGATIVE })
                    };
                    IndexManager.getProposalTable(positiveResults);
                    IndexManager.getProposalTable(informationResults);
                    IndexManager.getProposalTable(negativeResults);
                }

            })
        });
    },

    getProposalTable: function (data) {
        AjaxFunctionPOST(data, AJAX_ENDPOINTS.Proposal.RenderProposalTable, CONTENT_TYPES.JSON, SUCCESS_TYPES.DONOTING)().done(function (html) {
            switch (data.StatusType) {
                case PROPOSAL_STATUS_TYPES.POSITIVE:
                    $("#proposalResults #crdPositive").html(html);
                    break;
                case PROPOSAL_STATUS_TYPES.INFORMATION:
                    $("#proposalResults #crdInformation").html(html);
                    break;
                case PROPOSAL_STATUS_TYPES.NEGATIVE:
                    $("#proposalResults #crdNegative").html(html);
                    break;
                default:
                    break;
            }
        
        });
    }
}
$(document).ready(function () {
    IndexManager.init();
});