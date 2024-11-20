/**
 * BilliardClubParisien.Api
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 0.2.0
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */
import { HttpHeaders }                                       from '@angular/common/http';

import { Observable }                                        from 'rxjs';

import { CreateInscriptionRequest } from '../model/models';
import { CreateInscriptionResponse } from '../model/models';
import { GetAllInscriptionsResponse } from '../model/models';
import { GetInscriptionResponse } from '../model/models';
import { UpdateInscriptionRequest } from '../model/models';
import { UpdateInscriptionResponse } from '../model/models';


import { Configuration }                                     from '../configuration';



export interface InscriptionServiceInterface {
    defaultHeaders: HttpHeaders;
    configuration: Configuration;

    /**
     * 
     * 
     * @param createInscriptionRequest 
     */
    inscriptionCreate(createInscriptionRequest: CreateInscriptionRequest, extraHttpRequestParams?: any): Observable<CreateInscriptionResponse>;

    /**
     * 
     * 
     * @param id 
     */
    inscriptionGet(id: number, extraHttpRequestParams?: any): Observable<GetInscriptionResponse>;

    /**
     * 
     * 
     */
    inscriptionGetAllInscriptions(extraHttpRequestParams?: any): Observable<GetAllInscriptionsResponse>;

    /**
     * 
     * 
     * @param id 
     * @param updateInscriptionRequest 
     */
    inscriptionPatch(id: number, updateInscriptionRequest: UpdateInscriptionRequest, extraHttpRequestParams?: any): Observable<UpdateInscriptionResponse>;

}
