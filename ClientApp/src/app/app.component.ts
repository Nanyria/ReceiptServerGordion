import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterOutlet } from '@angular/router';
import { ReceiptService } from './Services/receipt.service';
import { Receipt } from './Models/interfaces';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule, RouterOutlet],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'ClientApp';

  receipts: Receipt[] = [];
  selectedReceipt: Receipt | null = null;
  queryId: number | null = null;
  loadingList = false;
  loadingReceipt = false;
  error: string | null = null;

  constructor(private receiptService: ReceiptService) {}

  ngOnInit(): void {
    this.loadReceipts();
  }

  loadReceipts(): void {
    this.loadingList = true;
    this.error = null;
    this.receiptService.getReceipts().subscribe({
      next: (res) => {
        // API may return either a plain array, or an envelope with `data` or `result`
        let list: any = [];
        if (Array.isArray(res)) {
          list = res;
        } else if (res && res.data) {
          list = res.data;
        } else if (res && res.result) {
          list = res.result;
        } else {
          list = [];
        }
        // Optional: normalize date strings to Date objects
        this.receipts = (list || []).map((r: any) => ({ ...r, date: r.date ? new Date(r.date) : r.date }));
        this.loadingList = false;
      },
      error: (err) => {
        this.error = 'Failed to load receipts';
        this.loadingList = false;
      }
    });
  }

  fetchReceipt(): void {
    if (this.queryId == null) {
      this.error = 'Please enter a receipt number';
      return;
    }
    this.loadingReceipt = true;
    this.error = null;
    this.receiptService.getReceiptById(this.queryId).subscribe({
      next: (res) => {
        // Handle envelope shapes: { data }, { result } or raw object
        if (res && res.data) {
          this.selectedReceipt = res.data;
        } else if (res && res.result) {
          this.selectedReceipt = res.result;
        } else {
          this.selectedReceipt = res;
        }
        // Normalize date to Date object for display
        if (this.selectedReceipt && this.selectedReceipt.date) {
          this.selectedReceipt.date = new Date(this.selectedReceipt.date);
        }
        this.loadingReceipt = false;
      },
      error: () => {
        this.error = 'Receipt not found';
        this.selectedReceipt = null;
        this.loadingReceipt = false;
      }
    });
  }


}
