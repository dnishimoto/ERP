"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
/*
@Injectable({
  providedIn: 'root',
})
*/
var ApplicationService = /** @class */ (function () {
    function ApplicationService(http) {
        this.http = http;
    }
    //http.get<IGeneralLedgerView>(baseUrl + 'api/GeneralLedger/ById/25').subscribe(result => {
    ApplicationService.prototype.getData = function () {
        return ('reached');
        //return this.http.get
    };
    ApplicationService.prototype.getLedgerById = function (id) {
        this.http.get('${DATA_ACCESS_PREFIX}/api/GeneralLedger/ById/' + id).subscribe(function (result) {
            return result;
        }, function (error) { return console.error(error); });
    };
    return ApplicationService;
}());
exports.ApplicationService = ApplicationService;
//# sourceMappingURL=application.service.js.map