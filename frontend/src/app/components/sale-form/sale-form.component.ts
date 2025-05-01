import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { SalesService } from '../../services/sales.service';
import { Sale } from '../../models/sale.model';

@Component({
  selector: 'app-sales-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './sale-form.component.html',
  styleUrls: ['./sale-form.component.scss']
})
export class SaleFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);
  public router = inject(Router);
  private salesService = inject(SalesService);

  form!: FormGroup;
  isEdit = false;
  isView = false;
  id = '';

  ngOnInit(): void {
    this.form = this.fb.group({
      saleNumber: ['', Validators.required],
      saleDate: ['', Validators.required],
      customer: ['', Validators.required],
      branch: ['', Validators.required],
      items: this.fb.array([]),
      totalAmount: [{ value: 0, disabled: true }],
      isCancelled: [false]
    });

    this.id = this.route.snapshot.paramMap.get('id') || '';
    this.isEdit = !!this.id;

    this.route.queryParamMap.subscribe((params) => {
      this.isView = params.get('view') === 'true';
      if (this.isView) this.form.disable();
    });

    if (this.isEdit) {
      this.salesService.getById(this.id).subscribe({
        next: (sale) => {
          const formattedDate = new Date(sale.saleDate).toISOString().slice(0, 16);
          this.form.patchValue({ ...sale, saleDate: formattedDate });
          this.setItems(sale.items || []);
          this.updateTotalAmount();
        },
        error: () => alert('Erro ao carregar venda.')
      });
    } else {
      this.addItem();
    }

    this.form.get('items')?.valueChanges.subscribe(() => this.updateTotalAmount());
  }

  get items(): FormArray {
    return this.form.get('items') as FormArray;
  }

  addItem(): void {
    const itemGroup = this.fb.group({
      product: ['', Validators.required],
      quantity: [1, [Validators.required, Validators.min(1)]],
      unitPrice: [0, [Validators.required, Validators.min(0)]],
    });

    this.items.push(itemGroup);
  }

  removeItem(index: number): void {
    this.items.removeAt(index);
  }

  setItems(items: any[]): void {
    this.items.clear();
    items.forEach(item => {
      const group = this.fb.group({
        id: [item.id],
        product: [item.product, Validators.required],
        quantity: [item.quantity, [Validators.required, Validators.min(1)]],
        unitPrice: [item.unitPrice, [Validators.required, Validators.min(0)]],
      });
      this.items.push(group);
    });
  }

  updateTotalAmount(): void {
    const total = this.items.controls.reduce((sum, ctrl) => {
      const { quantity, unitPrice } = ctrl.value;
      return sum + (quantity * unitPrice);
    }, 0);

    this.form.get('totalAmount')?.setValue(total);
  }

  onStatusChange(event: Event): void {
    const value = (event.target as HTMLSelectElement).value;
    this.form.get('isCancelled')?.setValue(value === 'true');
  }

  confirmCancel(index: number): void {
    if (confirm('Tem certeza que deseja cancelar o item da venda?')) {
      const item = this.items.at(index).value;
  
      this.salesService.cancelItem(this.id, item.id).subscribe({
        next: () => {
          alert('Item cancelado com sucesso.');
          this.items.removeAt(index);
        },
        error: () => alert('Erro ao cancelar item.')
      });
    }
  }

  onDelete(): void {
    if (!confirm('Tem certeza que deseja excluir esta venda?')) return;
  
    this.salesService.delete(this.id).subscribe({
      next: () => {
        alert('Venda excluída com sucesso!');
        this.router.navigate(['/']);
      },
      error: () => alert('Erro ao excluir venda.')
    });
  }

  onSubmit(): void {
    if (this.form.invalid) return;

    const sale: Sale = { ...this.form.getRawValue() };

    if (this.isEdit) {
      this.salesService.update(this.id, sale).subscribe({
        next: () => {
          alert('Venda atualizada com sucesso!');
          this.router.navigate(['/']);
        },
        error: () => alert('Erro ao atualizar venda.')
      });
    } else {
      this.salesService.create(sale).subscribe({
        next: () => {
          alert('Venda criada com sucesso!');
          this.router.navigate(['/']);
        },
        error: () => alert('Erro ao criar venda.')
      });
    }
  }
}
