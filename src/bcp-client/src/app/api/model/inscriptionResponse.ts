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
import { InscriptionImageResponse } from './inscriptionImageResponse';


export interface InscriptionResponse { 
    id: number;
    firstName?: string | null;
    lastName?: string | null;
    sex?: string | null;
    email?: string | null;
    phone?: string | null;
    isMemberBefore?: boolean;
    formula?: string | null;
    joinCompetition?: boolean;
    competitionCats?: Array<string> | null;
    motivation?: string | null;
    status?: string | null;
    dtCreate?: string | null;
    dtUpdate?: string | null;
    inscriptionImages?: Array<InscriptionImageResponse> | null;
}

