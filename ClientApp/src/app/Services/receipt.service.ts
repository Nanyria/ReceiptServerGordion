import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Receipt } from '../Models/interfaces';

@Injectable({
    providedIn: 'root'
})
export class ReceiptService {
    // Use a relative URL and a dev proxy to avoid hardcoded ports and CORS issues
    private apiUrl = '/api/receipt';

    constructor(private http: HttpClient) { }

    getReceipts(): Observable<any> {
        return this.http.get<any>(this.apiUrl);
    }

    getReceiptById(id: number): Observable<any> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.get<any>(url);
    }

    createReceipt(receipt: Receipt): Observable<any> {
        return this.http.post<any>(this.apiUrl, receipt);
    }

    updateReceipt(id: number, receipt: Receipt): Observable<any> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.put<any>(url, receipt);
    }

    deleteReceipt(id: number): Observable<any> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.delete<any>(url);
    }
}