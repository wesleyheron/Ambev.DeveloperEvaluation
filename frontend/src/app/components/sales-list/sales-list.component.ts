import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SalesService } from '../../services/sales.service';
import { Sale } from '../../models/sale.model';

@Component({
  selector: 'app-sales-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './sales-list.component.html',
  styleUrls: ['./sales-list.component.scss']
})
export class SalesListComponent implements OnInit {
  salesService = inject(SalesService);
  router = inject(RouterModule);

  sales: Sale[] = [];
  loading = true;
  error = '';

  ngOnInit() {
    this.salesService.getAll().subscribe({
      next: (data) => {
        console.log('Dados recebidos:', data);
        if (data) {
          this.sales = data;
          console.log('Sales:', this.sales);
        }
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Erro ao carregar vendas.';
        this.loading = false;
        console.error('Erro na requisição:', err);
      }
    });
  }

  viewDetails(id: string) {
    location.href = `/sales/${id}`;
  }

  editSale(id: string) {
    location.href = `/sales/edit/${id}`;
  }

  cancelSale(id: string) {
    if (confirm('Tem certeza que deseja cancelar esta venda?')) {
      this.salesService.cancel(id).subscribe({
        next: () => {
          alert('Venda cancelada com sucesso');
          this.sales = this.sales.filter(s => s.id !== id);
        },
        error: () => alert('Erro ao cancelar a venda')
      });
    }
  }
}
