
export interface ReceiptArticle {
	id: number;
	articleId: number;
	name: string;
	quantity: number;
	unitPrice: number;
	total: number;
}

export interface Receipt {
	id: number;
	date: string | Date;
	receiptArticles: ReceiptArticle[];
	totalAmount: number;
}

// Generic API envelope matching typical backend service responses
export interface ApiResponse<T> {
	statusCode: number;
	message?: string;
	data?: T;
	errors?: any;
}

