import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { SalesService } from '../../services/sales.service';
import { Sale } from '../../models/sale.model';

@Component({
  selector: 'app-sale-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './sale-detail.component.html',
  styleUrls: ['./sale-detail.component.scss']
})
export class SaleDetailComponent implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private salesService = inject(SalesService);

  sale: Sale | null = null;
  isLoading = true;

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id')!;
    this.salesService.getById(id).subscribe({
      next: (sale) => {
        this.sale = {
          ...sale,
          saleDate: new Date(sale.saleDate).toLocaleString(),
          totalAmount: sale.items?.reduce((sum, item) => sum + item.totalAmount, 0) ?? 0
        };
        this.isLoading = false;
        console.log(sale)
      },
      error: () => {
        alert('Erro ao carregar venda.');
        this.router.navigate(['/']);
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/']);
  }
}
